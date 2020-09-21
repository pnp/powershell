using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteCollectionAppCatalog")]
    public class RemoveSiteCollectionAppCatalog: PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            string url = null;
            if(Site.Site != null)
            {
                Site.Site.EnsureProperty(s => s.Url);
                url = Site.Site.Url;
            } else if(!string.IsNullOrEmpty(Site.Url))
            {
                url = Site.Url;
            }
            
            Tenant.GetSiteByUrl(url).RootWeb.TenantAppCatalog.SiteCollectionAppCatalogsSites.Remove(url);
            ClientContext.ExecuteQueryRetry();
        }
    }
}