using System.Management.Automation;
using System.Net.Http;
using System.Text.RegularExpressions;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPChangeLog", DefaultParameterSetName = ParameterSet_SpecificVersion)]
    [OutputType(typeof(string))]
    public partial class GetChangeLog : BasePSCmdlet
    {
        private const string ParameterSet_Nightly = "Current nightly";
        private const string ParameterSet_SpecificVersion = "Specific version";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Nightly)]
        public SwitchParameter Nightly;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SpecificVersion)]
        [Alias("Release")]
        public System.Version Version;

        protected override void ProcessRecord()
        {
            var client = Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            string releaseNotes;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Version)))
            {
                releaseNotes = RetrieveSpecificRelease(client, Version.ToString());
            }
            else if (Nightly)
            {
                releaseNotes = RetrieveSpecificRelease(client, "Current nightly");
            }
            else
            {
                releaseNotes = RetrieveLatestStableRelease(client);
            }

            WriteObject(releaseNotes);
        }

        /// <summary>
        /// Retrieves the changelog regarding the latest stable release from GitHub
        /// </summary>
        /// <param name="httpClient">HttpClient to use to request the data from GitHub</param>
        /// <exception cref="PSInvalidOperationException">Thrown if it is unable to parse the changelog data properly</exception>
        /// <returns>The changelog regarding the latest stable release</returns>
        private string RetrieveLatestStableRelease(HttpClient httpClient)
        {
            var url = "https://raw.githubusercontent.com/pnp/powershell/dev/CHANGELOG.md";

            LogDebug($"Retrieving changelog from {url}");

            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new PSInvalidOperationException("Failed to retrieve changelog from GitHub");
            }

            LogDebug("Successfully retrieved changelog from GitHub");

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var releasedVersions = Regex.Matches(content, @"## \[(?<version>\d+?\.\d+?\.\d+?)]");
            if (releasedVersions.Count == 0)
            {
                throw new PSInvalidOperationException("Failed to identify versions in changelog on GitHub");
            }

            LogDebug($"Found {releasedVersions.Count} released versions in changelog");
            LogDebug($"Looking for release information on previous stable version {releasedVersions[0].Groups["version"].Value}");

            var match = Regex.Match(content, @$"(?<changelog>## \[{releasedVersions[0].Groups["version"].Value.Replace(".", @"\.")}]\n.*?)\n## \[\d+?\.\d+?\.\d+?\]", RegexOptions.Singleline);

            if (!match.Success)
            {
                throw new PSInvalidOperationException($"Failed to identify changelog for version {releasedVersions[0].Groups["version"].Value} on GitHub");                    
            }

            return match.Groups["changelog"].Value;
        }            

        /// <summary>
        /// Retrieves the changelog regarding a specific release from GitHub
        /// </summary>
        /// <param name="httpClient">HttpClient to use to request the data from GitHub</param>
        /// <exception cref="PSInvalidOperationException">Thrown if it is unable to parse the changelog data properly</exception>
        /// <returns>The changelog regarding the specific release</returns>
        private string RetrieveSpecificRelease(HttpClient httpClient, string version)
        {
            var url = "https://raw.githubusercontent.com/pnp/powershell/dev/CHANGELOG.md";

            LogDebug($"Retrieving changelog from {url}");

            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new PSInvalidOperationException("Failed to retrieve changelog from GitHub");
            }

            LogDebug("Successfully retrieved changelog from GitHub");

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            LogDebug($"Looking for release information on the {version} release");

            var match = Regex.Match(content, @$"(?<changelog>## \[{version}]\n.*?)\n## \[\d+?\.\d+?\.\d+?\]", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new PSInvalidOperationException($"Failed to identify changelog for the {version} release on GitHub");                    
            }

            return match.Groups["changelog"].Value;
        }
    }
}