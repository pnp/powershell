using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Reflection;
using System.Xml.Linq;
using PnP.PowerShell.Commands.Base;

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
        private static readonly Uri NightlyVersionCheckJsonUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/dev/version.json");

        /// <summary>
        /// URL to the PnP PowerShell release notes for the stable release
        /// </summary>
        private static readonly Uri ReleaseVersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/master/version.txt");
        private static readonly Uri ReleaseVersionCheckJsonUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/master/version.json");

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
        public static void CheckVersion(BasePSCmdlet cmdlet)
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

                cmdlet?.LogDebug($"Checking for updates, current version is {productVersion}. See https://pnp.github.io/powershell/articles/configuration.html#disable-or-enable-version-checks for more information.");

                // Check for the latest available version
                var onlineVersion = GetAvailableVersion3(isNightly);
                if (onlineVersion != null)
                {

                    if (IsNewer(onlineVersion.SemanticVersion))
                    {
                        if (cmdlet != null)
                        {
                            var updateMessage = $"\nA newer version of PnP PowerShell is available: {onlineVersion.Version}.\n\nUse 'Update-Module -Name PnP.PowerShell{(isNightly ? " -AllowPrerelease" : "")}' to update.\nUse 'Get-PnPChangeLog {(!isNightly ? $"-Release {onlineVersion}" : "-Nightly")}' to list changes.\n\nYou can turn this check off by adding $env:PNPPOWERSHELL_UPDATECHECK='Off' to your PowerShell profile. See\n\nhttps://pnp.github.io/powershell/articles/configuration.html#disable-or-enable-version-checks\n\nfor more information.\n\n";
                            CmdletMessageWriter.WriteFormattedWarning(cmdlet, updateMessage);
                        }
                    }
                    else
                    {
                        cmdlet?.LogDebug($"No newer version of PnP PowerShell is available, latest available version is {onlineVersion.Version}");
                    }
                    if (!string.IsNullOrEmpty(onlineVersion.Message))
                    {
                        if (cmdlet != null)
                        {
                            CmdletMessageWriter.WriteFormattedMessage(cmdlet, new CmdletMessageWriter.Message() { Formatted = true, Text = onlineVersion.Message, Type = CmdletMessageWriter.MessageType.Message });
                        }
                    }
                }
                VersionChecked = true;
            }
            catch (Exception e)
            {
                cmdlet?.LogDebug($"Error checking for updates: {e.Message}");
            }
        }

        /// <summary>
        /// Checks if the provided version is newer than the current version
        /// </summary>
        /// <param name="availableVersionString">The version to check the current version against</param>
        /// <returns>True if the provided version is newer than the current version, false if it is not</returns>
        public static bool IsNewer(SemanticVersion availableVersion)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = versionInfo.ProductVersion;


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

        internal static PnPVersionResult GetAvailableVersion3(bool isNightly)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();

            // Deliberately lowering timeout as the version check is not critical so in case of a slower or blocked internet connection, this should not block the cmdlet for too long
            httpClient.Timeout = TimeSpan.FromSeconds(VersionCheckTimeOut);
            var request = new HttpRequestMessage(HttpMethod.Get, isNightly ? NightlyVersionCheckJsonUrl : ReleaseVersionCheckJsonUrl);

            var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var onlineVersionRaw = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var onlineVersion = System.Text.Json.JsonSerializer.Deserialize<PnPVersionResult>(onlineVersionRaw);
                return onlineVersion;
            }
            return null;
        }


        internal static string GetAvailableVersion2(bool isNightly)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();

            // Deliberately lowering timeout as the version check is not critical so in case of a slower or blocked internet connection, this should not block the cmdlet for too long
            httpClient.Timeout = TimeSpan.FromSeconds(VersionCheckTimeOut);
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.powershellgallery.com/api/v2/FindPackagesById()?id='PnP.PowerShell'&$top=10&$orderby=Created%20desc{(isNightly ? "" : "&$filter=IsPrerelease%20eq%20false")}");
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
                if (entry != null)
                {
                    var properties = entry.Elements(metadataNS + "properties").FirstOrDefault();
                    if (properties != null)
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
        public static PnPVersionResult GetAvailableVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = versionInfo.ProductVersion;
            var isNightly = productVersion.Contains("-");

            return GetAvailableVersion3(isNightly);
        }
    }

    public class PnPVersionResult
    {
        public string Version { get; set; }
        public SemanticVersion SemanticVersion
        {
            get
            {
                SemanticVersion.TryParse(Version, out SemanticVersion result);
                return result;
            }
        }
        public string Message { get; set; }
    }
}