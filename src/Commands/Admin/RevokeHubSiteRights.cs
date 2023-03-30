using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPHubSiteRights")]
    public class RevokeHubSiteRights : PnPAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public HubSitePipeBind Identity { get; set; }

        [Parameter(Mandatory = true)]
        public string[] Principals { get; set; }

        protected override void ExecuteCmdlet()
        {
            base.Tenant.RevokeHubSiteRights(Identity.Url ?? Identity.GetHubSite(Tenant).SiteUrl, Principals);
            AdminContext.ExecuteQueryRetry();
        }
    }
}