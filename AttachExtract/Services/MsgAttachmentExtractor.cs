using System.Collections.Concurrent;
using Aspose.Email;
using AttachExtract.Models;

namespace AttachExtract.Services;

/// <summary>
/// Loads MSG files with Aspose.Email, renders them to PDF via Aspose.Words (through an
/// intermediate MHTML stream) and/or saves their attachments to disk. Safe to drive from
/// multiple threads: each MSG file is processed independently and output file names are
/// reserved through a thread-safe registry to avoid collisions.
/// </summary>
public sealed class MsgAttachmentExtractor
{
    private const string FallbackExtension = ".dat";

    /// <summary>
    /// Processes every given MSG file in parallel: renders it to PDF and/or extracts its
    /// attachments, depending on <paramref name="mode"/>.
    /// </summary>
    /// <param name="msgFilePaths">Full paths of the .msg files to process.</param>
    /// <param name="destinationFolder">Folder that will receive the PDFs and/or attachments.</param>
    /// <param name="mode">Whether to only create a PDF, or create a PDF and extract attachments.</param>
    /// <param name="maxDegreeOfParallelism">Maximum number of MSG files processed at once.</param>
    /// <param name="progress">Reports one update per completed MSG file.</param>
    /// <param name="cancellationToken">Allows the caller to cancel an in-flight run.</param>
    public Task<ExtractionSummary> ExtractAllAsync(
        IReadOnlyList<string> msgFilePaths,
        string destinationFolder,
        ProcessingMode mode,
        int maxDegreeOfParallelism,
        IProgress<FileProcessedEventArgs> progress,
        CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var results = new ConcurrentBag<MsgProcessingResult>();
            var reservedPaths = new ConcurrentDictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
            int completed = 0;

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Math.Max(1, maxDegreeOfParallelism),
                CancellationToken = cancellationToken
            };

            Parallel.ForEach(msgFilePaths, parallelOptions, msgFilePath =>
            {
                MsgProcessingResult result = ProcessSingleFile(msgFilePath, destinationFolder, mode, reservedPaths, cancellationToken);
                results.Add(result);

                int completedCount = Interlocked.Increment(ref completed);
                progress.Report(new FileProcessedEventArgs
                {
                    Result = result,
                    CompletedCount = completedCount,
                    TotalCount = msgFilePaths.Count
                });
            });

            return new ExtractionSummary(results.ToList());
        }, cancellationToken);
    }

    private static MsgProcessingResult ProcessSingleFile(
        string msgFilePath,
        string destinationFolder,
        ProcessingMode mode,
        ConcurrentDictionary<string, byte> reservedPaths,
        CancellationToken cancellationToken)
    {
        string baseName = Path.GetFileNameWithoutExtension(msgFilePath);

        try
        {
            using MailMessage message = MailMessage.Load(msgFilePath);
            cancellationToken.ThrowIfCancellationRequested();

            int savedAttachments = 0;
            var notes = new List<string>();

            // Render the MSG itself to PDF. Named after the MSG file so it can always be
            // traced back to its source e-mail, just like the extracted attachments below.
            // Aspose.Email cannot save a MailMessage straight to PDF, so the message is first
            // saved to MHTML in memory, then Aspose.Words renders that MHTML to PDF.
            string pdfPath = ReserveUniquePath(Path.Combine(destinationFolder, $"{baseName}.pdf"), reservedPaths);
            using (var mhtmlStream = new MemoryStream())
            {
                message.Save(mhtmlStream, SaveOptions.DefaultMhtml);
                mhtmlStream.Position = 0;

                var mhtmlLoadOptions = new Aspose.Words.LoadOptions { LoadFormat = Aspose.Words.LoadFormat.Mhtml };
                var wordsDocument = new Aspose.Words.Document(mhtmlStream, mhtmlLoadOptions);
                wordsDocument.Save(pdfPath, new Aspose.Words.Saving.PdfSaveOptions());
            }
            notes.Add("PDF created.");
            cancellationToken.ThrowIfCancellationRequested();

            if (mode == ProcessingMode.PdfAndAttachments)
            {
                int attachmentCount = message.Attachments.Count;
                if (attachmentCount == 0)
                {
                    notes.Add("No attachments found.");
                }
                else
                {
                    for (int i = 0; i < attachmentCount; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        Attachment attachment = message.Attachments[i];

                        string extension = Path.GetExtension(attachment.Name);
                        if (string.IsNullOrWhiteSpace(extension))
                        {
                            extension = FallbackExtension;
                        }

                        // Attachments are named after the MSG file itself (not the attachment's
                        // own name) so the extracted files can always be traced back to their
                        // source e-mail. A -0001, -0002, ... suffix is only added once a MSG has
                        // more than one attachment.
                        string desiredFileName = attachmentCount == 1
                            ? $"{baseName}{extension}"
                            : $"{baseName}-{i + 1:D4}{extension}";

                        string desiredPath = Path.Combine(destinationFolder, desiredFileName);
                        string targetPath = ReserveUniquePath(desiredPath, reservedPaths);

                        attachment.Save(targetPath);
                        savedAttachments++;
                    }

                    notes.Add($"{savedAttachments} attachment(s) extracted.");
                }
            }

            return MsgProcessingResult.Success(msgFilePath, pdfCreated: true, savedAttachments, string.Join(" ", notes));
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            return MsgProcessingResult.Failure(msgFilePath, ex.Message);
        }
    }

    /// <summary>
    /// Reserves a unique output path, appending " (2)", " (3)", ... when the desired name is
    /// already taken by a file on disk or by another thread processing a same-named MSG file
    /// (e.g. two "Invoice.msg" files coming from different sub-folders).
    /// </summary>
    private static string ReserveUniquePath(string desiredPath, ConcurrentDictionary<string, byte> reservedPaths)
    {
        string directory = Path.GetDirectoryName(desiredPath)!;
        string nameOnly = Path.GetFileNameWithoutExtension(desiredPath);
        string extension = Path.GetExtension(desiredPath);

        string candidate = desiredPath;
        int suffix = 2;

        while (true)
        {
            bool reservedNow = reservedPaths.TryAdd(candidate, 0);
            if (reservedNow && !File.Exists(candidate))
            {
                return candidate;
            }

            candidate = Path.Combine(directory, $"{nameOnly} ({suffix}){extension}");
            suffix++;
        }
    }
}
