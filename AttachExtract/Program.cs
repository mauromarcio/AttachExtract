using AttachExtract.Services;

namespace AttachExtract;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Loads a licensed Aspose.Total / Aspose.Email .lic file if one is present next to the
        // executable. Falls back to evaluation mode automatically when no license file is found.
        AsposeLicenseManager.TryApplyLicense();

        Application.Run(new MainForm());
    }
}
