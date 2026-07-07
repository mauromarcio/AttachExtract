namespace AttachExtract;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer? components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.headerPanel = new System.Windows.Forms.Panel();
        this.lblAppSubtitle = new System.Windows.Forms.Label();
        this.lblAppTitle = new System.Windows.Forms.Label();
        this.optionsPanel = new System.Windows.Forms.Panel();
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnStart = new System.Windows.Forms.Button();
        this.numMaxParallelism = new System.Windows.Forms.NumericUpDown();
        this.lblMaxParallelism = new System.Windows.Forms.Label();
        this.btnBrowseDestination = new System.Windows.Forms.Button();
        this.txtDestinationFolder = new System.Windows.Forms.TextBox();
        this.lblDestinationFolder = new System.Windows.Forms.Label();
        this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
        this.btnBrowseSource = new System.Windows.Forms.Button();
        this.txtSourceFolder = new System.Windows.Forms.TextBox();
        this.lblSourceFolder = new System.Windows.Forms.Label();
        this.dgvResults = new System.Windows.Forms.DataGridView();
        this.colMsgFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colAttachments = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
        this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsCountsLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.headerPanel.SuspendLayout();
        this.optionsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numMaxParallelism)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
        this.statusStrip1.SuspendLayout();
        this.SuspendLayout();
        //
        // headerPanel
        //
        this.headerPanel.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.headerPanel.Controls.Add(this.lblAppSubtitle);
        this.headerPanel.Controls.Add(this.lblAppTitle);
        this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.headerPanel.Location = new System.Drawing.Point(0, 0);
        this.headerPanel.Name = "headerPanel";
        this.headerPanel.Size = new System.Drawing.Size(1024, 64);
        this.headerPanel.TabIndex = 0;
        //
        // lblAppSubtitle
        //
        this.lblAppSubtitle.AutoSize = true;
        this.lblAppSubtitle.BackColor = System.Drawing.Color.Transparent;
        this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        this.lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(219, 234, 254);
        this.lblAppSubtitle.Location = new System.Drawing.Point(26, 38);
        this.lblAppSubtitle.Name = "lblAppSubtitle";
        this.lblAppSubtitle.Size = new System.Drawing.Size(300, 17);
        this.lblAppSubtitle.TabIndex = 1;
        this.lblAppSubtitle.Text = "Bulk attachment extractor for Outlook MSG files";
        //
        // lblAppTitle
        //
        this.lblAppTitle.AutoSize = true;
        this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;
        this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
        this.lblAppTitle.ForeColor = System.Drawing.Color.White;
        this.lblAppTitle.Location = new System.Drawing.Point(22, 8);
        this.lblAppTitle.Name = "lblAppTitle";
        this.lblAppTitle.Size = new System.Drawing.Size(160, 28);
        this.lblAppTitle.TabIndex = 0;
        this.lblAppTitle.Text = "AttachExtract";
        //
        // optionsPanel
        //
        this.optionsPanel.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
        this.optionsPanel.Controls.Add(this.btnCancel);
        this.optionsPanel.Controls.Add(this.btnStart);
        this.optionsPanel.Controls.Add(this.numMaxParallelism);
        this.optionsPanel.Controls.Add(this.lblMaxParallelism);
        this.optionsPanel.Controls.Add(this.btnBrowseDestination);
        this.optionsPanel.Controls.Add(this.txtDestinationFolder);
        this.optionsPanel.Controls.Add(this.lblDestinationFolder);
        this.optionsPanel.Controls.Add(this.chkIncludeSubfolders);
        this.optionsPanel.Controls.Add(this.btnBrowseSource);
        this.optionsPanel.Controls.Add(this.txtSourceFolder);
        this.optionsPanel.Controls.Add(this.lblSourceFolder);
        this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.optionsPanel.Location = new System.Drawing.Point(0, 64);
        this.optionsPanel.Name = "optionsPanel";
        this.optionsPanel.Size = new System.Drawing.Size(1024, 184);
        this.optionsPanel.TabIndex = 1;
        //
        // btnCancel
        //
        this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCancel.Enabled = false;
        this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancel.Location = new System.Drawing.Point(914, 132);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(90, 32);
        this.btnCancel.TabIndex = 10;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        //
        // btnStart
        //
        this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnStart.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnStart.FlatAppearance.BorderSize = 0;
        this.btnStart.ForeColor = System.Drawing.Color.White;
        this.btnStart.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
        this.btnStart.Location = new System.Drawing.Point(764, 132);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(140, 32);
        this.btnStart.TabIndex = 9;
        this.btnStart.Text = "Start Extraction";
        this.btnStart.UseVisualStyleBackColor = false;
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        //
        // numMaxParallelism
        //
        this.numMaxParallelism.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.numMaxParallelism.Location = new System.Drawing.Point(180, 135);
        this.numMaxParallelism.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
        this.numMaxParallelism.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numMaxParallelism.Name = "numMaxParallelism";
        this.numMaxParallelism.Size = new System.Drawing.Size(60, 23);
        this.numMaxParallelism.TabIndex = 8;
        this.numMaxParallelism.Value = new decimal(new int[] { 1, 0, 0, 0 });
        //
        // lblMaxParallelism
        //
        this.lblMaxParallelism.AutoSize = true;
        this.lblMaxParallelism.Location = new System.Drawing.Point(20, 138);
        this.lblMaxParallelism.Name = "lblMaxParallelism";
        this.lblMaxParallelism.Size = new System.Drawing.Size(96, 15);
        this.lblMaxParallelism.TabIndex = 7;
        this.lblMaxParallelism.Text = "Parallel workers:";
        //
        // btnBrowseDestination
        //
        this.btnBrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnBrowseDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnBrowseDestination.Location = new System.Drawing.Point(914, 89);
        this.btnBrowseDestination.Name = "btnBrowseDestination";
        this.btnBrowseDestination.Size = new System.Drawing.Size(90, 25);
        this.btnBrowseDestination.TabIndex = 6;
        this.btnBrowseDestination.Text = "Browse…";
        this.btnBrowseDestination.UseVisualStyleBackColor = true;
        this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
        //
        // txtDestinationFolder
        //
        this.txtDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.txtDestinationFolder.Location = new System.Drawing.Point(180, 90);
        this.txtDestinationFolder.Name = "txtDestinationFolder";
        this.txtDestinationFolder.ReadOnly = true;
        this.txtDestinationFolder.Size = new System.Drawing.Size(724, 23);
        this.txtDestinationFolder.TabIndex = 5;
        //
        // lblDestinationFolder
        //
        this.lblDestinationFolder.AutoSize = true;
        this.lblDestinationFolder.Location = new System.Drawing.Point(20, 93);
        this.lblDestinationFolder.Name = "lblDestinationFolder";
        this.lblDestinationFolder.Size = new System.Drawing.Size(105, 15);
        this.lblDestinationFolder.TabIndex = 4;
        this.lblDestinationFolder.Text = "Destination folder:";
        //
        // chkIncludeSubfolders
        //
        this.chkIncludeSubfolders.AutoSize = true;
        this.chkIncludeSubfolders.Location = new System.Drawing.Point(180, 54);
        this.chkIncludeSubfolders.Name = "chkIncludeSubfolders";
        this.chkIncludeSubfolders.Size = new System.Drawing.Size(128, 19);
        this.chkIncludeSubfolders.TabIndex = 3;
        this.chkIncludeSubfolders.Text = "Include subfolders";
        this.chkIncludeSubfolders.UseVisualStyleBackColor = true;
        //
        // btnBrowseSource
        //
        this.btnBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnBrowseSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnBrowseSource.Location = new System.Drawing.Point(914, 19);
        this.btnBrowseSource.Name = "btnBrowseSource";
        this.btnBrowseSource.Size = new System.Drawing.Size(90, 25);
        this.btnBrowseSource.TabIndex = 2;
        this.btnBrowseSource.Text = "Browse…";
        this.btnBrowseSource.UseVisualStyleBackColor = true;
        this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
        //
        // txtSourceFolder
        //
        this.txtSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.txtSourceFolder.Location = new System.Drawing.Point(180, 20);
        this.txtSourceFolder.Name = "txtSourceFolder";
        this.txtSourceFolder.ReadOnly = true;
        this.txtSourceFolder.Size = new System.Drawing.Size(724, 23);
        this.txtSourceFolder.TabIndex = 1;
        //
        // lblSourceFolder
        //
        this.lblSourceFolder.AutoSize = true;
        this.lblSourceFolder.Location = new System.Drawing.Point(20, 23);
        this.lblSourceFolder.Name = "lblSourceFolder";
        this.lblSourceFolder.Size = new System.Drawing.Size(140, 15);
        this.lblSourceFolder.TabIndex = 0;
        this.lblSourceFolder.Text = "Source folder (.msg files):";
        //
        // dgvResults
        //
        this.dgvResults.AllowUserToAddRows = false;
        this.dgvResults.AllowUserToDeleteRows = false;
        this.dgvResults.AllowUserToResizeRows = false;
        this.dgvResults.BackgroundColor = System.Drawing.Color.White;
        this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMsgFile,
            this.colStatus,
            this.colAttachments,
            this.colDetails});
        this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvResults.EnableHeadersVisualStyles = false;
        this.dgvResults.Location = new System.Drawing.Point(0, 248);
        this.dgvResults.MultiSelect = false;
        this.dgvResults.Name = "dgvResults";
        this.dgvResults.ReadOnly = true;
        this.dgvResults.RowHeadersVisible = false;
        this.dgvResults.RowTemplate.Height = 25;
        this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvResults.Size = new System.Drawing.Size(1024, 385);
        this.dgvResults.TabIndex = 2;
        //
        // colMsgFile
        //
        this.colMsgFile.HeaderText = "MSG File";
        this.colMsgFile.Name = "colMsgFile";
        this.colMsgFile.ReadOnly = true;
        this.colMsgFile.Width = 280;
        //
        // colStatus
        //
        this.colStatus.HeaderText = "Status";
        this.colStatus.Name = "colStatus";
        this.colStatus.ReadOnly = true;
        this.colStatus.Width = 90;
        //
        // colAttachments
        //
        this.colAttachments.HeaderText = "Attachments";
        this.colAttachments.Name = "colAttachments";
        this.colAttachments.ReadOnly = true;
        this.colAttachments.Width = 110;
        //
        // colDetails
        //
        this.colDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.colDetails.HeaderText = "Details";
        this.colDetails.Name = "colDetails";
        this.colDetails.ReadOnly = true;
        //
        // statusStrip1
        //
        this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsStatusLabel,
            this.tsCountsLabel});
        this.statusStrip1.Location = new System.Drawing.Point(0, 633);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new System.Drawing.Size(1024, 22);
        this.statusStrip1.TabIndex = 3;
        //
        // tsProgressBar
        //
        this.tsProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Left;
        this.tsProgressBar.Name = "tsProgressBar";
        this.tsProgressBar.Size = new System.Drawing.Size(200, 16);
        //
        // tsStatusLabel
        //
        this.tsStatusLabel.Name = "tsStatusLabel";
        this.tsStatusLabel.Size = new System.Drawing.Size(692, 17);
        this.tsStatusLabel.Spring = true;
        this.tsStatusLabel.Text = "Ready.";
        this.tsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // tsCountsLabel
        //
        this.tsCountsLabel.Name = "tsCountsLabel";
        this.tsCountsLabel.Size = new System.Drawing.Size(30, 17);
        this.tsCountsLabel.Text = "   ";
        //
        // MainForm
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1024, 655);
        this.Controls.Add(this.dgvResults);
        this.Controls.Add(this.statusStrip1);
        this.Controls.Add(this.optionsPanel);
        this.Controls.Add(this.headerPanel);
        this.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.MinimumSize = new System.Drawing.Size(900, 620);
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "AttachExtract — Outlook MSG Attachment Extractor";
        this.headerPanel.ResumeLayout(false);
        this.headerPanel.PerformLayout();
        this.optionsPanel.ResumeLayout(false);
        this.optionsPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numMaxParallelism)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Panel headerPanel;
    private System.Windows.Forms.Label lblAppSubtitle;
    private System.Windows.Forms.Label lblAppTitle;
    private System.Windows.Forms.Panel optionsPanel;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.NumericUpDown numMaxParallelism;
    private System.Windows.Forms.Label lblMaxParallelism;
    private System.Windows.Forms.Button btnBrowseDestination;
    private System.Windows.Forms.TextBox txtDestinationFolder;
    private System.Windows.Forms.Label lblDestinationFolder;
    private System.Windows.Forms.CheckBox chkIncludeSubfolders;
    private System.Windows.Forms.Button btnBrowseSource;
    private System.Windows.Forms.TextBox txtSourceFolder;
    private System.Windows.Forms.Label lblSourceFolder;
    private System.Windows.Forms.DataGridView dgvResults;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMsgFile;
    private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    private System.Windows.Forms.DataGridViewTextBoxColumn colAttachments;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDetails;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
    private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
    private System.Windows.Forms.ToolStripStatusLabel tsCountsLabel;
}
