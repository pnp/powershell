using System;
using System.Management.Automation;
using System.Net.Http;
using System.Reflection;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class VersionChecker
    {

#if DEBUG
        private static readonly Uri VersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/dev/version.txt");
#else
        private static readonly Uri VersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/master/version.txt");
#endif
        private static bool VersionChecked;

        public static void CheckVersion(PSCmdlet cmdlet)
        {
            // do we need to check versions. Is the environment variable set?
            var pnppowershellUpdatecheck = Environment.GetEnvironmentVariable("PNPPOWERSHELL_UPDATECHECK");
            if (!string.IsNullOrEmpty(pnppowershellUpdatecheck))
            {
                if (pnppowershellUpdatecheck.ToLower() == "off" || pnppowershellUpdatecheck.ToLower() == "false")
                {
                    VersionChecked = true;
                }
            }

            try
            {
                if (!VersionChecked)
                {
                    var onlineVersion = GetAvailableVersion();

                    if (isNewer(onlineVersion) && cmdlet != null)
                    {
#if DEBUG
                        var updateMessage = $"\nA newer version of PnP PowerShell is available: {onlineVersion}.\n\nUse 'Update-Module -Name PnP.PowerShell -AllowPrerelease' to update.\n\nYou can turn this check off by setting the 'PNPPOWERSHELL_UPDATECHECK' environment variable to 'Off'.\n";
#else
                        var updateMessage = $"\nA newer version of PnP PowerShell is available: {onlineVersion}.\n\nUse 'Update-Module -Name PnP.PowerShell' to update.\n\nYou can turn this check off by setting the 'PNPPOWERSHELL_UPDATECHECK' environment variable to 'Off'.\n";
#endif
                        CmdletMessageWriter.WriteFormattedWarning(cmdlet, updateMessage);
                    }
                    VersionChecked = true;
                }
            }
            catch (Exception)
            { }
        }

        public static bool isNewer(string availableVersionString)
        {
            var assembly = Assembly.GetExecutingAssembly();
#if !NETFRAMEWORK
            var currentVersion = new SemanticVersion(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            if (SemanticVersion.TryParse(availableVersionString, out SemanticVersion availableVersion))
#else
            var currentVersion = new Version(((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version);
            if (Version.TryParse(availableVersionString, out Version availableVersion))
#endif
            {
                if (availableVersion.Major > currentVersion.Major)
                {
                    return true;
                }
                else
                {
                    if (availableVersion.Major == currentVersion.Major && availableVersion.Minor > currentVersion.Minor)
                    {
                        return true;
                    }
#if !NETFRAMEWORK
                    else
                    {
                        if (!string.IsNullOrEmpty(currentVersion.PreReleaseLabel))
                        {
                            if (availableVersion.Major == currentVersion.Major && availableVersion.Minor == currentVersion.Minor && availableVersion.Patch > currentVersion.Patch)
                            {
                                return true;
                            }
                        }
                    }
#endif
                }
            }
            return false;
        }

        public static string GetAvailableVersion()
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            var response = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, VersionCheckUrl)).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var onlineVersion = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                onlineVersion = onlineVersion.Trim(new char[] { '\t', '\r', '\n' });
                return onlineVersion;
            }
            return null;
        }
    }
}