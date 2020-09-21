using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantSyncClientRestriction")]
    public class GetPnPTenantSyncClientRestriction : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(Tenant);
            ClientContext.Load(Tenant, t => t.HideDefaultThemes);
            ClientContext.ExecuteQueryRetry();
            WriteObject(new SPOTenantSyncClientRestriction(Tenant));
        }
    }
}