using System.Management.Automation;
using System.Net.Http;

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
            var response = client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                WriteObject(content);
            }
            else
            {
                throw new PSInvalidOperationException("Cannot retrieve changelog");
            }
        }
    }
}