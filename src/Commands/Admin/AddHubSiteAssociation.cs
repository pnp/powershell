using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPHubSiteAssociation")]
    public class AddHubSiteAssociation : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        public SitePipeBind HubSite;

        protected override void ExecuteCmdlet()
        {
            Tenant.ConnectSiteToHubSite(Site.Url, HubSite.Url);
            AdminContext.ExecuteQueryRetry();
        }
    }
}
