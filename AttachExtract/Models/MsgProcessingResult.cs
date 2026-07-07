namespace AttachExtract.Models;

/// <summary>
/// The outcome of extracting attachments from a single MSG file.
/// </summary>
public sealed class MsgProcessingResult
{
    public required string MsgFilePath { get; init; }
    public required bool Succeeded { get; init; }
    public int AttachmentsExtracted { get; init; }
    public required string Message { get; init; }

    public string MsgFileName => Path.GetFileName(MsgFilePath);

    public static MsgProcessingResult Success(string msgFilePath, int attachmentsExtracted, string message) =>
        new()
        {
            MsgFilePath = msgFilePath,
            Succeeded = true,
            AttachmentsExtracted = attachmentsExtracted,
            Message = message
        };

    public static MsgProcessingResult Failure(string msgFilePath, string message) =>
        new()
        {
            MsgFilePath = msgFilePath,
            Succeeded = false,
            AttachmentsExtracted = 0,
            Message = message
        };
}
