using Aspose.Email;

namespace AttachExtract.Services;

/// <summary>
/// Applies a licensed Aspose.Total / Aspose.Email .lic file at startup, if one can be found.
/// The same file is used to license both Aspose.Email (MSG loading/attachments) and
/// Aspose.Words (used internally to render the MHTML-to-PDF conversion). The application
/// still runs in evaluation mode when no license is present.
/// </summary>
internal static class AsposeLicenseManager
{
    private static readonly string[] CandidateLicenseFileNames =
    {
        "Aspose.Total.lic",
        "Aspose.Email.lic"
    };

    public static bool TryApplyLicense()
    {
        string? licensePath = CandidateLicenseFileNames
            .Select(fileName => Path.Combine(AppContext.BaseDirectory, fileName))
            .FirstOrDefault(File.Exists);

        if (licensePath is null)
        {
            return false;
        }

        bool applied = false;

        try
        {
            new License().SetLicense(licensePath);
            applied = true;
        }
        catch (Exception)
        {
            // Invalid or corrupt license file for Aspose.Email: continue in evaluation mode.
        }

        try
        {
            new Aspose.Words.License().SetLicense(licensePath);
            applied = true;
        }
        catch (Exception)
        {
            // Invalid or corrupt license file for Aspose.Words: continue in evaluation mode.
        }

        return applied;
    }
}
