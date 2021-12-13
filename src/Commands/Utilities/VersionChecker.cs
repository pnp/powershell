using System;
using System.Diagnostics;
using System.Management.Automation;
using System.Net.Http;
using System.Reflection;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class VersionChecker
    {

        private static readonly Uri NightlyVersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/dev/version.txt");
        private static readonly Uri ReleaseVersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/master/version.txt");
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
                    var assembly = Assembly.GetExecutingAssembly();
                    var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                    var productVersion = versionInfo.ProductVersion;
                    var isNightly = productVersion.Contains("-");

                    var onlineVersion = GetAvailableVersion(isNightly);

                    if (IsNewer(onlineVersion) && cmdlet != null)
                    {
                        var updateMessage = $"\nA newer version of PnP PowerShell is available: {onlineVersion}.\n\nUse 'Update-Module -Name PnP.PowerShell{(isNightly ? " -AllowPrerelease" : "")}' to update.\nUse 'Get-PnPChangeLog {(!isNightly ? $"-Release {onlineVersion}" : "-Nightly")}' to list changes.\n\nYou can turn this check off by setting the 'PNPPOWERSHELL_UPDATECHECK' environment variable to 'Off'.\n";
                        CmdletMessageWriter.WriteFormattedWarning(cmdlet, updateMessage);
                    }
                    VersionChecked = true;
                }
            }
            catch (Exception)
            { }
        }

        public static bool IsNewer(string availableVersionString)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = versionInfo.ProductVersion;


            if (Version.TryParse(availableVersionString, out Version availableVersion))
            {
                if (availableVersion.Major > versionInfo.ProductMajorPart)
                {
                    return true;
                }
                else
                {
                    if (versionInfo.ProductMajorPart == availableVersion.Major && availableVersion.Minor > versionInfo.ProductMinorPart)
                    {
                        return true;
                    }
                    else
                    {
                        if (productVersion.Contains("-"))
                        {
                            if (versionInfo.ProductMajorPart == availableVersion.Major && versionInfo.ProductMinorPart == availableVersion.Minor && availableVersion.Build > versionInfo.ProductBuildPart)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        // #if !NETFRAMEWORK
        //             var currentVersion = new SemanticVersion(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
        //             if (SemanticVersion.TryParse(availableVersionString, out SemanticVersion availableVersion))
        // #else
        //             var currentVersion = new Version(((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version);
        //             if (Version.TryParse(availableVersionString, out Version availableVersion))
        // #endif
        //             {
        //                 if (availableVersion.Major > currentVersion.Major)
        //                 {
        //                     return true;
        //                 }
        //                 else
        //                 {
        //                     if (availableVersion.Major == currentVersion.Major && availableVersion.Minor > currentVersion.Minor)
        //                     {
        //                         return true;
        //                     }
        // #if !NETFRAMEWORK
        //                     else
        //                     {
        //                         if (!string.IsNullOrEmpty(currentVersion.PreReleaseLabel))
        //                         {
        //                             if (availableVersion.Major == currentVersion.Major && availableVersion.Minor == currentVersion.Minor && availableVersion.Patch > currentVersion.Patch)
        //                             {
        //                                 return true;
        //                             }
        //                         }
        //                     }
        // #endif
        //                 }
        //             }
        //     return false;
        // }

        internal static string GetAvailableVersion(bool isNightly)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            var response = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, isNightly ? NightlyVersionCheckUrl : ReleaseVersionCheckUrl)).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var onlineVersion = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                onlineVersion = onlineVersion.Trim(new char[] { '\t', '\r', '\n' });
                return onlineVersion;
            }
            return null;
        }
        public static string GetAvailableVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = versionInfo.ProductVersion;
            var isNightly = productVersion.Contains("-");
            return GetAvailableVersion(isNightly);
        }
    }
}