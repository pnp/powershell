using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPOrgAssetsLibrary")]
    public class GetOrgAssetsLibrary : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetOrgAssets();
            ClientContext.ExecuteQueryRetry();
            WriteObject(results.Value, true);
        }
    }
}