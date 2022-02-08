using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHubSiteChild")]
    public class GetHubSiteChild : PnPAdminCmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        public HubSitePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            HubSiteProperties hubSiteProperties;
            try
            {
                hubSiteProperties = Identity.GetHubSite(Tenant);
                hubSiteProperties.EnsureProperty(h => h.ID);
            }
            catch (ServerException ex)
            {
                if (ex.ServerErrorTypeName.Equals("System.IO.FileNotFoundException"))
                {
                    throw new ArgumentException(Resources.SiteNotFound, nameof(Identity));
                }
                throw;
            }

            // Get the ID of the hubsite for which we need to find child sites
            var hubSiteId = hubSiteProperties.ID;

            WriteObject(Tenant.GetHubSiteChildUrls(hubSiteId), true);
        }
    }
}
