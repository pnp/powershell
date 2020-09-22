using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteScriptFromList")]
    public class GetSiteScriptFromList : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Url;

        protected override void ExecuteCmdlet()
        {
            var script = Tenant.GetSiteScriptFromList(ClientContext, Url);
            ClientContext.ExecuteQueryRetry();
            WriteObject(script.Value);
        }
    }
}