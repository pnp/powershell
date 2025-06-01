using System;
using System.Linq;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Unregister, "PnPHubSite")]
    public class UnregisterHubSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            var hubSitesProperties = Tenant.GetHubSitesProperties();
            AdminContext.Load(hubSitesProperties);
            AdminContext.ExecuteQueryRetry();
            HubSiteProperties props = null;
            if (Site.Id != Guid.Empty)
            {
                props = hubSitesProperties.Single(h => h.SiteId == Site.Id);
            }
            else
            {
                props = hubSitesProperties.Single(h => !string.IsNullOrEmpty(h.SiteUrl) && h.SiteUrl.Equals(Site.Url, StringComparison.OrdinalIgnoreCase));
            }
            Tenant.UnregisterHubSiteById(props.ID);
            AdminContext.ExecuteQueryRetry();
        }
    }
}