using Aspose.Email;

namespace AttachExtract.Services;

/// <summary>
/// Applies a licensed Aspose.Total / Aspose.Email .lic file at startup, if one can be found.
/// The application still runs in evaluation mode when no license is present.
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
        foreach (string fileName in CandidateLicenseFileNames)
        {
            string candidatePath = Path.Combine(AppContext.BaseDirectory, fileName);
            if (!File.Exists(candidatePath))
            {
                continue;
            }

            try
            {
                new License().SetLicense(candidatePath);
                return true;
            }
            catch (Exception)
            {
                // Invalid or corrupt license file: continue in evaluation mode instead of crashing.
            }
        }

        return false;
    }
}
