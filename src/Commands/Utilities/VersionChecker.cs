using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Functionality to take care of checking if a newer version of PnP PowerShell is available
    /// </summary>
    public static class VersionChecker
    {
        /// <summary>
        /// URL to the PnP PowerShell release notes for the nightly release
        /// </summary>
        private static readonly Uri NightlyVersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/dev/version.txt");

        /// <summary>
        /// URL to the PnP PowerShell release notes for the stable release
        /// </summary>
        private static readonly Uri ReleaseVersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/master/version.txt");

        /// <summary>
        /// Boolean to indicate if the version check has already been performed
        /// </summary>
        private static bool VersionChecked;

        /// <summary>
        /// Timeout in seconds to allow for the version check to be performed at most. If it exceeds this time, the check will silently be skipped. Verbose output will show when this happens.
        /// </summary>
        public static short VersionCheckTimeOut = 10;
        private static readonly char[] trimChars = ['\t', '\r', '\n'];

        /// <summary>
        /// Performs the check for a newer PnP PowerShell version
        /// </summary>
        /// <param name="cmdlet">Cmdlet instance from which this check is done</param>
        public static void CheckVersion(PSCmdlet cmdlet)
        {
            // Do we need to check versions: is the environment variable set?
            var pnppowershellUpdatecheck = Environment.GetEnvironmentVariable("PNPPOWERSHELL_UPDATECHECK");
            if (!string.IsNullOrEmpty(pnppowershellUpdatecheck))
            {
                // If the environment variable is set to false or off, we don't need to check
                if (pnppowershellUpdatecheck.ToLower() == "off" || pnppowershellUpdatecheck.ToLower() == "false")
                {
                    VersionChecked = true;
                }
            }

            // If the version has already been checked, no need to do so again
            if (VersionChecked) return;

            try
            {
                // Get the current version of PnP PowerShell being used
                var assembly = Assembly.GetExecutingAssembly();
                var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                var productVersion = versionInfo.ProductVersion;
                var isNightly = productVersion.Contains("-");

                cmdlet?.WriteVerbose($"Checking for updates, current version is {productVersion}. See https://pnp.github.io/powershell/articles/configuration.html#disable-or-enable-version-checks for more information.");

                // Check for the latest available version
                var onlineVersion = GetAvailableVersion2(isNightly);

                if (IsNewer(onlineVersion))
                {
                    if (cmdlet != null)
                    {
                        var updateMessage = $"\nA newer version of PnP PowerShell is available: {onlineVersion}.\n\nUse 'Update-Module -Name PnP.PowerShell{(isNightly ? " -AllowPrerelease" : "")}' to update.\nUse 'Get-PnPChangeLog {(!isNightly ? $"-Release {onlineVersion}" : "-Nightly")}' to list changes.\n\nYou can turn this check off by setting the 'PNPPOWERSHELL_UPDATECHECK' environment variable to 'Off'.\n";
                        CmdletMessageWriter.WriteFormattedWarning(cmdlet, updateMessage);
                    }
                }
                else
                {
                    cmdlet?.WriteVerbose($"No newer version of PnP PowerShell is available, latest available version is {onlineVersion}");
                }
                VersionChecked = true;
            }
            catch (Exception e)
            {
                cmdlet?.WriteVerbose($"Error checking for updates: {e.Message}");
            }
        }

        /// <summary>
        /// Checks if the provided version is newer than the current version
        /// </summary>
        /// <param name="availableVersionString">The version to check the current version against</param>
        /// <returns>True if the provided version is newer than the current version, false if it is not</returns>
        public static bool IsNewer(string availableVersionString)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = versionInfo.ProductVersion;        
            
            if (SemanticVersion.TryParse(availableVersionString, out SemanticVersion availableVersion))
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
                            if (versionInfo.ProductMajorPart == availableVersion.Major && versionInfo.ProductMinorPart == availableVersion.Minor && availableVersion.Patch > versionInfo.ProductBuildPart)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves the latest available version of PnP PowerShell. Based on the provided isNightly flag, it will check for the latest nightly or stable release.
        /// </summary>
        /// <returns>The latest available version</returns>
        internal static string GetAvailableVersion(bool isNightly)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            
            // Deliberately lowering timeout as the version check is not critical so in case of a slower or blocked internet connection, this should not block the cmdlet for too long
            httpClient.Timeout = TimeSpan.FromSeconds(VersionCheckTimeOut);
            var request = new HttpRequestMessage(HttpMethod.Get, isNightly ? NightlyVersionCheckUrl : ReleaseVersionCheckUrl)
            {
                Version = new Version(2, 0)
            };

            var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var onlineVersion = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                onlineVersion = onlineVersion.Trim(trimChars);
                return onlineVersion;
            }
            return null;
        }

        internal static string GetAvailableVersion2(bool isNightly)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            
            // Deliberately lowering timeout as the version check is not critical so in case of a slower or blocked internet connection, this should not block the cmdlet for too long
            httpClient.Timeout = TimeSpan.FromSeconds(VersionCheckTimeOut);
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.powershellgallery.com/api/v2/FindPackagesById()?id='PnP.PowerShell'&$top=10&$orderby=Created%20desc");
            request.Version = new Version(2, 0);
            var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                XNamespace atomNS = "http://www.w3.org/2005/Atom";
                XNamespace metadataNS = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
                XNamespace dataServicesNS = "http://schemas.microsoft.com/ado/2007/08/dataservices";
                var onlineVersion = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var xml = XDocument.Parse(onlineVersion);
                var entry = xml.Root.Elements(atomNS + "entry").FirstOrDefault();
                if(entry!= null)
                {
                    var properties = entry.Elements(metadataNS + "properties").FirstOrDefault();
                    if(properties != null)
                    {
                        var version = properties.Element(dataServicesNS + "Version").Value;
                        return version;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the latest available version of PnP PowerShell. If the current version is a nightly build, it will check for the latest nightly build as well. If the current version is a stable build, it will only check for the latest stable build.
        /// </summary>
        /// <returns>The latest available version</returns>
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