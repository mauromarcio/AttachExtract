namespace AttachExtract.Models;

/// <summary>
/// What to produce in the destination folder for each MSG file.
/// </summary>
public enum ProcessingMode
{
    /// <summary>Only render the MSG itself to PDF; attachments are left untouched.</summary>
    PdfOnly,

    /// <summary>Render the MSG to PDF and also extract its attachments.</summary>
    PdfAndAttachments
}
