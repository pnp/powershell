using System.Management.Automation;
using System.Reflection;
using System.Text.Json;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPChangeLog")]
    [OutputType(typeof(string))]
    public class GetChangeLog : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Nightly;

        [Parameter(Mandatory = false)]
        public System.Version Release;

        protected override void ProcessRecord()
        {
            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Release)))
            {
                var url = $"https://api.github.com/repos/pnp/powershell/releases/tags/v{Release.Major}.{Release.Minor}.{Release.Build}";
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
                    if (jsonElement.TryGetProperty("body", out JsonElement bodyElement))
                    {
                        WriteObject(bodyElement.GetString());
                    }
                }
            }
            else
            {
                var url = "https://raw.githubusercontent.com/pnp/powershell/master/CHANGELOG.md";
                if (Nightly)
                {
                    url = "https://raw.githubusercontent.com/pnp/powershell/dev/CHANGELOG.md";
                }
                var assembly = Assembly.GetExecutingAssembly();
                var currentVersion = new SemanticVersion(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (Nightly)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(content, @"## \[Current Nightly\][\r\n]{1,}(.*?)[\r\n]{1,}## \[[\d\.]{5,}][\r\n]{1,}", System.Text.RegularExpressions.RegexOptions.Singleline);

                        if (match.Success)
                        {
                            WriteObject(match.Groups[1].Value);
                        }
                    }
                    else
                    {
                        var currentVersionString = $"{currentVersion.Major}.{currentVersion.Minor}.0";
                        var previousVersionString = $"{currentVersion.Major}.{currentVersion.Minor - 1}.0";
                        var match = System.Text.RegularExpressions.Regex.Match(content, $"(## \\[{currentVersionString}\\]\\n(.*)\\n)(## \\[{previousVersionString}]\\n)", System.Text.RegularExpressions.RegexOptions.Singleline);

                        if (match.Success)
                        {
                            WriteObject(match.Groups[1].Value);
                        }
                    }
                }
                else
                {
                    throw new PSInvalidOperationException("Cannot retrieve changelog");
                }
            }
        }
    }
}