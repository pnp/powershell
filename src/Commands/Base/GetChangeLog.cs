using System.Management.Automation;
using System.Net.Http;
using System.Reflection;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPChangeLog")]
    public class GetChangeLog : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Nightly;
        protected override void ProcessRecord()
        {
            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            var url = "https://raw.githubusercontent.com/pnp/powershell/master/CHANGELOG.md";
            if (Nightly)
            {
                url = "https://raw.githubusercontent.com/pnp/powershell/dev/CHANGELOG.md";
            }

            var assembly = Assembly.GetExecutingAssembly();
#if !NETFRAMEWORK
            var currentVersion = new SemanticVersion(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
#else
            var currentVersion = new System.Version(((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version);
#endif

            var response = client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (Nightly)
                {
                    var versionString = $"{currentVersion.Major}.{currentVersion.Minor}.0";
                    var match = System.Text.RegularExpressions.Regex.Match(content, $"(## \\[Current Nightly\\]\\n(.*)\\n)(## \\[{versionString}]\\n)", System.Text.RegularExpressions.RegexOptions.Singleline);

                    if (match.Success)
                    {
                        WriteObject(match.Groups[1].Value);
                    }
                }
                else
                {
                    var currentVersionString = $"{currentVersion.Major}.{currentVersion.Minor}.0";
                    var previousVersionString = $"{currentVersion.Major}.{currentVersion.Minor-1}.0";
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