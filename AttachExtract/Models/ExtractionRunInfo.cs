namespace AttachExtract.Models;

/// <summary>
/// Everything needed to write a human-readable audit log for one extraction run,
/// from the moment the user clicked Start to the moment the run finished.
/// </summary>
public sealed class ExtractionRunInfo
{
    public required DateTime StartedAt { get; init; }
    public required DateTime FinishedAt { get; init; }
    public required TimeSpan Duration { get; init; }
    public required string RunStatus { get; init; }
    public required string SourceFolder { get; init; }
    public required bool IncludeSubfolders { get; init; }
    public required string DestinationFolder { get; init; }
    public required string ModeDescription { get; init; }
    public required int MaxDegreeOfParallelism { get; init; }
    public required IReadOnlyList<MsgProcessingResult> Results { get; init; }
}
