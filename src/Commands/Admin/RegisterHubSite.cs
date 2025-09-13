using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using Microsoft.Identity.Client;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Register, "PnPHubSite")]
    public class RegisterHubSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        [Parameter(Mandatory = false)]
        [AllowEmptyCollection]
        [AllowNull]
        public string[] Principals { get; set; }

        protected override void ExecuteCmdlet()
        {
            SiteProperties siteProperties = null;
            if (Site.Id != Guid.Empty)
            {
                siteProperties = Tenant.GetSitePropertiesById(Site.Id,false, Connection.TenantAdminUrl);
                if (siteProperties == null) return;
            }

            HubSiteProperties hubSiteProperties = null;

            if (Site.Id != Guid.Empty)
            {
                hubSiteProperties = Tenant.RegisterHubSite(siteProperties.Url);
            }
            else
            {
                hubSiteProperties = Tenant.RegisterHubSite(Site.Url);
            }
            
            AdminContext.Load(hubSiteProperties);
            AdminContext.ExecuteQueryRetry();

            if (Principals != null && Principals.Length > 0)
            {
                try
                {
                    hubSiteProperties = Tenant.GrantHubSiteRightsById(hubSiteProperties.ID, Principals, SPOHubSiteUserRights.Join);
                    AdminContext.Load(hubSiteProperties);
                    AdminContext.ExecuteQueryRetry();
                }
                catch (Exception)
                {
                    if (Site.Id != Guid.Empty)
                    {
                        Tenant.UnregisterHubSite(siteProperties.Url);
                    }
                    else 
                    {
                        Tenant.UnregisterHubSite(Site.Url);
                    }
                        AdminContext.ExecuteQueryRetry();
                    throw;
                }                
            }

            WriteObject(hubSiteProperties);
        }
    }
}