namespace AttachExtract.Models;

/// <summary>
/// The outcome of processing a single MSG file (PDF conversion and/or attachment extraction).
/// </summary>
public sealed class MsgProcessingResult
{
    public required string MsgFilePath { get; init; }
    public required bool Succeeded { get; init; }
    public bool PdfCreated { get; init; }
    public int AttachmentsExtracted { get; init; }
    public required string Message { get; init; }

    public string MsgFileName => Path.GetFileName(MsgFilePath);

    public static MsgProcessingResult Success(string msgFilePath, bool pdfCreated, int attachmentsExtracted, string message) =>
        new()
        {
            MsgFilePath = msgFilePath,
            Succeeded = true,
            PdfCreated = pdfCreated,
            AttachmentsExtracted = attachmentsExtracted,
            Message = message
        };

    public static MsgProcessingResult Failure(string msgFilePath, string message) =>
        new()
        {
            MsgFilePath = msgFilePath,
            Succeeded = false,
            PdfCreated = false,
            AttachmentsExtracted = 0,
            Message = message
        };
}
