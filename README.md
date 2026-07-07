# AttachExtract

A Windows Forms (.NET 9) utility that bulk-converts Outlook `.msg` files to
PDF and/or extracts their attachments, using Aspose.Email and Aspose.Words
(both part of Aspose.Total), with multithreaded processing and live progress
reporting.

## Requirements

- Windows 10/11
- Visual Studio 2022 (17.8+) with the **.NET Desktop Development** workload
- .NET 9 SDK
- A licensed `Aspose.Total.lic` or `Aspose.Email.lic` file (optional — the app
  runs in Aspose evaluation mode if no license is found)

## Getting started

1. Open `AttachExtract.sln` in Visual Studio 2022.
2. Restore NuGet packages (Visual Studio does this automatically on build;
   it pulls `Aspose.Email` and `Aspose.Words` 24.10.x, both covered by your
   Aspose.Total license).
3. Optional: copy your licensed `Aspose.Total.lic` (or `Aspose.Email.lic`)
   file into the `AttachExtract` project folder, next to `AttachExtract.csproj`.
   The build copies it to the output folder automatically, and the app loads
   it at startup (see `Services/AsposeLicenseManager.cs`). License files are
   excluded from source control via `.gitignore` — never commit one.
4. Build and run (F5).

## Using the app

1. **Source folder** — Browse to the folder containing the `.msg` files.
   Check **Include subfolders** to search recursively.
2. **Processing mode** — choose:
   - *Only create PDF from MSG files* — renders each MSG to PDF; attachments
     are left untouched.
   - *Create PDF from MSG files while extracting the attachments* — renders
     each MSG to PDF **and** extracts its attachments (default).
3. **Destination folder** — Browse to (or create) the folder that will
   receive the PDFs and/or extracted attachments.
4. **Parallel workers** — how many MSG files to process concurrently
   (defaults to your machine's logical processor count).
5. Click **Start Processing**. The status bar shows a progress bar, the file
   currently being processed, and a running "N / total processed" counter.
   The grid lists a result row per MSG file (success/error, whether a PDF was
   created, attachment count, details). **Cancel** stops the run after
   in-flight files finish.

Every run also writes a timestamped log file — see below.

## File naming convention

Both the PDF and any extracted attachments are named after the **source MSG
file**, not the attachment's own file name, so you can always tell which
e-mail a file came from:

- PDF: `<MsgFileName>.pdf`
- Single attachment: `<MsgFileName><original extension>`
- Multiple attachments: `<MsgFileName>-0001<ext>`, `<MsgFileName>-0002<ext>`, ...

If a name collision would occur (e.g. two same-named MSG files from different
sub-folders, or an attachment that would otherwise collide with the PDF), a
` (2)`, ` (3)`, ... suffix is appended automatically so no existing file is
ever overwritten.

## Run log

Each run writes `AttachExtract_Log_yyyyMMdd_HHmmss.txt` into the destination
folder, recording the start/finish timestamps, total duration, the
source/destination folders, the processing mode and parallelism used, a
summary (succeeded/failed/PDFs created/attachments extracted), and a
per-file breakdown. It is written even if the run is cancelled or fails
partway through, using whatever files were processed up to that point.
See `Services/ExtractionLogWriter.cs`.

## Architecture notes

- `Services/MsgAttachmentExtractor.cs` — core processing logic, decoupled
  from the UI. Uses `Parallel.ForEach` with a caller-supplied
  `MaxDegreeOfParallelism` and a `CancellationToken`. Output file names are
  reserved through a `ConcurrentDictionary` so concurrent workers never race
  on the same destination path (PDF vs. attachments included).
- PDF rendering: Aspose.Email cannot save a `MailMessage` directly to PDF, so
  the message is first saved to an in-memory MHTML stream
  (`MailMessage.Save(stream, SaveOptions.DefaultMhtml)`), then Aspose.Words
  loads that stream (`LoadFormat.Mhtml`) and saves it as PDF
  (`Document.Save(path, new PdfSaveOptions())`).
- `MainForm` reports progress via `Progress<T>`, which automatically marshals
  callbacks back onto the UI thread — no manual `Invoke`/`BeginInvoke` needed,
  and no cross-thread control access. The same progress callback also
  accumulates per-file results for the run log.
- Each MSG file is processed independently and failures are captured
  per-file, so one corrupt/locked MSG file doesn't abort the whole batch.
