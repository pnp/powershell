using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Core.Model;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPHubSiteAssociation")]
    public class AddHubSiteAssociation : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        public SitePipeBind HubSite;

        protected override void ExecuteCmdlet()
        {
            try
            {
                Tenant.ConnectSiteToHubSite(Site.Url, HubSite.Url);
                AdminContext.ExecuteQueryRetry();
            }
            catch
            {
                try
                {
                    using (var primaryHub = PnPContext.Clone(HubSite.Url))
                    {
                        var primaryHubSite = primaryHub.Site.Get(p => p.HubSiteId, p => p.IsHubSite);

                        using (var associateHubSite = PnPContext.Clone(Site.Url))
                        {
                            var associateSite = associateHubSite.Site.Get(p => p.HubSiteId, p => p.IsHubSite);
                            if (associateSite.HubSiteId == Guid.Empty)
                            {
                                var resultJoin = associateSite.JoinHubSite(primaryHubSite.HubSiteId);
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
