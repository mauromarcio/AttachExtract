namespace AttachExtract.Models;

/// <summary>
/// Progress payload reported after each MSG file finishes processing.
/// </summary>
public sealed class FileProcessedEventArgs
{
    public required MsgProcessingResult Result { get; init; }
    public required int CompletedCount { get; init; }
    public required int TotalCount { get; init; }
}
