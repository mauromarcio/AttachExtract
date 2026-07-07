# AttachExtract

A Windows Forms (.NET 9) utility that bulk-extracts attachments from Outlook
`.msg` files using Aspose.Email (part of Aspose.Total), with multithreaded
processing and live progress reporting.

## Requirements

- Windows 10/11
- Visual Studio 2022 (17.8+) with the **.NET Desktop Development** workload
- .NET 9 SDK
- A licensed `Aspose.Total.lic` or `Aspose.Email.lic` file (optional — the app
  runs in Aspose evaluation mode if no license is found)

## Getting started

1. Open `AttachExtract.sln` in Visual Studio 2022.
2. Restore NuGet packages (Visual Studio does this automatically on build;
   it pulls `Aspose.Email` 24.10.x, which your Aspose.Total license covers).
3. Optional: copy your licensed `Aspose.Total.lic` (or `Aspose.Email.lic`)
   file into the `AttachExtract` project folder, next to `AttachExtract.csproj`.
   The build copies it to the output folder automatically, and the app loads
   it at startup (see `Services/AsposeLicenseManager.cs`). License files are
   excluded from source control via `.gitignore` — never commit one.
4. Build and run (F5).

## Using the app

1. **Source folder** — Browse to the folder containing the `.msg` files.
   Check **Include subfolders** to search recursively.
2. **Destination folder** — Browse to (or create) the folder that will
   receive the extracted attachments.
3. **Parallel workers** — how many MSG files to process concurrently
   (defaults to your machine's logical processor count).
4. Click **Start Extraction**. The status bar shows a progress bar, the file
   currently being processed, and a running "N / total processed" counter.
   The grid lists a result row per MSG file (success/error, attachment count,
   details). **Cancel** stops the run after in-flight files finish.

## Attachment naming convention

Extracted files are named after the **source MSG file**, not the attachment's
own file name, so you can always tell which e-mail an attachment came from:

- Single attachment: `<MsgFileName><original extension>`
- Multiple attachments: `<MsgFileName>-0001<ext>`, `<MsgFileName>-0002<ext>`, ...

If a name collision would occur (e.g. two same-named MSG files from different
sub-folders), a ` (2)`, ` (3)`, ... suffix is appended automatically so no
existing file is ever overwritten.

## Architecture notes

- `Services/MsgAttachmentExtractor.cs` — core extraction logic, decoupled
  from the UI. Uses `Parallel.ForEach` with a caller-supplied
  `MaxDegreeOfParallelism` and a `CancellationToken`. Output file names are
  reserved through a `ConcurrentDictionary` so concurrent workers never race
  on the same destination path.
- `MainForm` reports progress via `Progress<T>`, which automatically marshals
  callbacks back onto the UI thread — no manual `Invoke`/`BeginInvoke` needed,
  and no cross-thread control access.
- Each MSG file is processed independently and failures are captured
  per-file, so one corrupt/locked MSG file doesn't abort the whole batch.
