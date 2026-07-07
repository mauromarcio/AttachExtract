using System.Text;
using AttachExtract.Models;

namespace AttachExtract.Services;

/// <summary>
/// Writes a human-readable audit log describing what a run did and how long it took.
/// </summary>
public static class ExtractionLogWriter
{
    /// <summary>
    /// Writes the log file into <see cref="ExtractionRunInfo.DestinationFolder"/> and returns
    /// the full path that was written.
    /// </summary>
    public static string WriteLog(ExtractionRunInfo runInfo)
    {
        var sb = new StringBuilder();

        sb.AppendLine("AttachExtract run log");
        sb.AppendLine("======================");
        sb.AppendLine($"Started  : {runInfo.StartedAt:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"Finished : {runInfo.FinishedAt:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"Duration : {runInfo.Duration:hh\\:mm\\:ss}");
        sb.AppendLine($"Status   : {runInfo.RunStatus}");
        sb.AppendLine();
        sb.AppendLine($"Source folder      : {runInfo.SourceFolder}");
        sb.AppendLine($"Include subfolders : {(runInfo.IncludeSubfolders ? "Yes" : "No")}");
        sb.AppendLine($"Destination folder : {runInfo.DestinationFolder}");
        sb.AppendLine($"Processing mode    : {runInfo.ModeDescription}");
        sb.AppendLine($"Parallel workers   : {runInfo.MaxDegreeOfParallelism}");
        sb.AppendLine();
        sb.AppendLine("Summary");
        sb.AppendLine("-------");
        sb.AppendLine($"MSG files processed   : {runInfo.Results.Count}");
        sb.AppendLine($"Succeeded             : {runInfo.Results.Count(r => r.Succeeded)}");
        sb.AppendLine($"Failed                : {runInfo.Results.Count(r => !r.Succeeded)}");
        sb.AppendLine($"PDF files created     : {runInfo.Results.Count(r => r.PdfCreated)}");
        sb.AppendLine($"Attachments extracted : {runInfo.Results.Sum(r => r.AttachmentsExtracted)}");
        sb.AppendLine();
        sb.AppendLine("Details");
        sb.AppendLine("-------");

        foreach (MsgProcessingResult result in runInfo.Results)
        {
            string tag = result.Succeeded ? "OK   " : "ERROR";
            sb.AppendLine($"[{tag}] {result.MsgFileName,-45} {result.Message}");
        }

        string logFileName = $"AttachExtract_Log_{runInfo.StartedAt:yyyyMMdd_HHmmss}.txt";
        string logPath = Path.Combine(runInfo.DestinationFolder, logFileName);
        File.WriteAllText(logPath, sb.ToString());
        return logPath;
    }
}
