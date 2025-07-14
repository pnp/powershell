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
    public class GetHubSiteChild : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = false)]
        public HubSitePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            HubSiteProperties hubSiteProperties;
            try
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    hubSiteProperties = Identity.GetHubSite(Tenant);
                }
                else
                {
                    hubSiteProperties = Tenant.GetHubSitePropertiesByUrl(Connection.Url);
                }
                hubSiteProperties.EnsureProperty(h => h.ID);
            }
            catch (ServerObjectNullReferenceException)
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    throw new ArgumentException($"Unable to retrieve hub child sites of site provided through -{nameof(Identity)}. This could be caused by the site not being a hub site.", nameof(Identity));
                }
                else
                {
                    throw new PSInvalidOperationException($"Unable to retrieve hub child sites of the current site {Connection.Url}. This could be caused by this site not being a hub site.");
                }
            }
            catch (ServerException e) when (e.ServerErrorTypeName.Equals("System.IO.FileNotFoundException"))
            {
                throw new ArgumentException(Resources.SiteNotFound, nameof(Identity));
            }

            // Get the ID of the hubsite for which we need to find child sites
            var hubSiteId = hubSiteProperties.ID;

            WriteObject(Tenant.GetHubSiteChildUrls(hubSiteId, Connection.TenantAdminUrl), true);
        }
    }
}
