using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPHubSiteAssociation")]
    public class RemoveHubSiteAssociation : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            Tenant.DisconnectSiteFromHubSite(Site.Url);
            AdminContext.ExecuteQueryRetry();
        }
    }
}