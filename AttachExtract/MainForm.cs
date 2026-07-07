using System.Diagnostics;
using AttachExtract.Models;
using AttachExtract.Services;

namespace AttachExtract;

public partial class MainForm : Form
{
    private readonly MsgAttachmentExtractor _extractor = new();
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isProcessing;

    public MainForm()
    {
        InitializeComponent();

        dgvResults.AutoGenerateColumns = false;
        numMaxParallelism.Value = Math.Clamp(
            Environment.ProcessorCount,
            (int)numMaxParallelism.Minimum,
            (int)numMaxParallelism.Maximum);

        // Default to the mode that preserves the original attachment-extraction behavior.
        cmbProcessingMode.SelectedIndex = 1;
    }

    private ProcessingMode SelectedProcessingMode =>
        cmbProcessingMode.SelectedIndex == 0
            ? ProcessingMode.PdfOnly
            : ProcessingMode.PdfAndAttachments;

    private void btnBrowseSource_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select the folder that contains the MSG files to process.",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = false
        };

        if (!string.IsNullOrWhiteSpace(txtSourceFolder.Text) && Directory.Exists(txtSourceFolder.Text))
        {
            dialog.SelectedPath = txtSourceFolder.Text;
        }

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            txtSourceFolder.Text = dialog.SelectedPath;
        }
    }

    private void btnBrowseDestination_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select or create the folder where extracted attachments will be saved.",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = true
        };

        if (!string.IsNullOrWhiteSpace(txtDestinationFolder.Text) && Directory.Exists(txtDestinationFolder.Text))
        {
            dialog.SelectedPath = txtDestinationFolder.Text;
        }

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            txtDestinationFolder.Text = dialog.SelectedPath;
        }
    }

    private async void btnStart_Click(object? sender, EventArgs e)
    {
        if (_isProcessing)
        {
            return;
        }

        string sourceFolder = txtSourceFolder.Text.Trim();
        string destinationFolder = txtDestinationFolder.Text.Trim();

        if (!Directory.Exists(sourceFolder))
        {
            MessageBox.Show(this, "Please select a valid source folder that contains MSG files.",
                "Source folder required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(destinationFolder))
        {
            MessageBox.Show(this, "Please select or create a destination folder for the extracted attachments.",
                "Destination folder required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Directory.CreateDirectory(destinationFolder);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not create the destination folder:\n{ex.Message}",
                "Destination folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SearchOption searchOption = chkIncludeSubfolders.Checked
            ? SearchOption.AllDirectories
            : SearchOption.TopDirectoryOnly;

        List<string> msgFiles;
        try
        {
            msgFiles = Directory.EnumerateFiles(sourceFolder, "*.msg", searchOption).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not read the source folder:\n{ex.Message}",
                "Source folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (msgFiles.Count == 0)
        {
            MessageBox.Show(this, "No MSG files were found in the selected source folder.",
                "Nothing to process", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        dgvResults.Rows.Clear();
        tsProgressBar.Minimum = 0;
        tsProgressBar.Maximum = msgFiles.Count;
        tsProgressBar.Value = 0;
        tsCountsLabel.Text = $"0 / {msgFiles.Count} processed";
        tsStatusLabel.Text = "Starting extraction…";
        SetProcessingState(true);

        ProcessingMode mode = SelectedProcessingMode;
        int maxDegreeOfParallelism = (int)numMaxParallelism.Value;
        var collectedResults = new List<MsgProcessingResult>();

        _cancellationTokenSource = new CancellationTokenSource();
        var progress = new Progress<FileProcessedEventArgs>(e =>
        {
            collectedResults.Add(e.Result);
            OnFileProcessed(e);
        });

        DateTime startedAt = DateTime.Now;
        var stopwatch = Stopwatch.StartNew();
        string runStatus = "Completed";

        try
        {
            ExtractionSummary summary = await _extractor.ExtractAllAsync(
                msgFiles,
                destinationFolder,
                mode,
                maxDegreeOfParallelism,
                progress,
                _cancellationTokenSource.Token);

            stopwatch.Stop();
            tsStatusLabel.Text =
                $"Completed in {stopwatch.Elapsed:mm\\:ss}. " +
                $"{summary.SuccessfulFiles} succeeded, {summary.FailedFiles} failed, " +
                $"{summary.TotalPdfsCreated} PDF(s) created, " +
                $"{summary.TotalAttachmentsExtracted} attachment(s) extracted.";
        }
        catch (OperationCanceledException)
        {
            runStatus = "Cancelled by user";
            tsStatusLabel.Text = "Extraction cancelled by user.";
        }
        catch (Exception ex)
        {
            runStatus = $"Failed: {ex.Message}";
            MessageBox.Show(this, $"An unexpected error occurred:\n{ex.Message}",
                "Extraction error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            tsStatusLabel.Text = "Extraction failed.";
        }
        finally
        {
            stopwatch.Stop();
            DateTime finishedAt = DateTime.Now;

            var runInfo = new ExtractionRunInfo
            {
                StartedAt = startedAt,
                FinishedAt = finishedAt,
                Duration = stopwatch.Elapsed,
                RunStatus = runStatus,
                SourceFolder = sourceFolder,
                IncludeSubfolders = chkIncludeSubfolders.Checked,
                DestinationFolder = destinationFolder,
                ModeDescription = cmbProcessingMode.Text,
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                Results = collectedResults
            };

            try
            {
                string logPath = ExtractionLogWriter.WriteLog(runInfo);
                tsStatusLabel.Text += $" Log saved: {Path.GetFileName(logPath)}";
            }
            catch (Exception ex)
            {
                tsStatusLabel.Text += $" (Could not write log file: {ex.Message})";
            }

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            SetProcessingState(false);
        }
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        if (_cancellationTokenSource is { IsCancellationRequested: false })
        {
            tsStatusLabel.Text = "Cancelling…";
            _cancellationTokenSource.Cancel();
        }
    }

    private void OnFileProcessed(FileProcessedEventArgs e)
    {
        MsgProcessingResult result = e.Result;

        int rowIndex = dgvResults.Rows.Add(
            result.MsgFileName,
            result.Succeeded ? "Success" : "Error",
            result.PdfCreated ? "Yes" : "No",
            result.AttachmentsExtracted,
            result.Message);

        if (!result.Succeeded)
        {
            dgvResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Firebrick;
        }

        dgvResults.FirstDisplayedScrollingRowIndex = rowIndex;

        tsProgressBar.Value = Math.Min(e.CompletedCount, tsProgressBar.Maximum);
        tsCountsLabel.Text = $"{e.CompletedCount} / {e.TotalCount} processed";
        tsStatusLabel.Text = $"Processing: {result.MsgFileName}";
    }

    private void SetProcessingState(bool isProcessing)
    {
        _isProcessing = isProcessing;

        txtSourceFolder.Enabled = !isProcessing;
        txtDestinationFolder.Enabled = !isProcessing;
        btnBrowseSource.Enabled = !isProcessing;
        btnBrowseDestination.Enabled = !isProcessing;
        chkIncludeSubfolders.Enabled = !isProcessing;
        cmbProcessingMode.Enabled = !isProcessing;
        numMaxParallelism.Enabled = !isProcessing;
        btnStart.Enabled = !isProcessing;
        btnCancel.Enabled = isProcessing;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isProcessing)
        {
            DialogResult result = MessageBox.Show(this,
                "Extraction is still in progress. Do you want to cancel it and exit?",
                "Extraction in progress", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            _cancellationTokenSource?.Cancel();
        }

        base.OnFormClosing(e);
    }
}
