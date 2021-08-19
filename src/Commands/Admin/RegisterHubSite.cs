using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Register, "PnPHubSite")]
    public class RegisterHubSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        [Parameter(Mandatory = false)]
        [AllowEmptyCollection]
        [AllowNull]
        public string[] Principals { get; set; }

        protected override void ExecuteCmdlet()
        {
            HubSiteProperties hubSiteProperties = Tenant.RegisterHubSite(Site.Url);
            ClientContext.Load(hubSiteProperties);
            ClientContext.ExecuteQueryRetry();

            if (Principals != null && Principals.Length > 0)
            {
                try
                {
                    hubSiteProperties = Tenant.GrantHubSiteRightsById(hubSiteProperties.ID, Principals, SPOHubSiteUserRights.Join);
                    ClientContext.Load(hubSiteProperties);
                    ClientContext.ExecuteQueryRetry();
                }
                catch (Exception)
                {
                    Tenant.UnregisterHubSite(Site.Url);
                    ClientContext.ExecuteQueryRetry();
                    throw;
                }                
            }

            WriteObject(hubSiteProperties);
        }
    }
}