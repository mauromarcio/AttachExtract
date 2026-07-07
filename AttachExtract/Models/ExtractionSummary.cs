namespace AttachExtract.Models;

/// <summary>
/// Aggregated results for a full extraction run across all selected MSG files.
/// </summary>
public sealed class ExtractionSummary
{
    public ExtractionSummary(IReadOnlyList<MsgProcessingResult> results)
    {
        Results = results;
    }

    public IReadOnlyList<MsgProcessingResult> Results { get; }

    public int TotalFiles => Results.Count;
    public int SuccessfulFiles => Results.Count(r => r.Succeeded);
    public int FailedFiles => Results.Count(r => !r.Succeeded);
    public int TotalPdfsCreated => Results.Count(r => r.PdfCreated);
    public int TotalAttachmentsExtracted => Results.Sum(r => r.AttachmentsExtracted);
}
