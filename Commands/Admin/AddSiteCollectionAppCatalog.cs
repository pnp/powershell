using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteCollectionAppCatalog")]
    
    
    [CmdletRelatedLink(Text = "Documentation", Url = "https://docs.microsoft.com/sharepoint/dev/general-development/site-collection-app-catalog#configure-and-manage-site-collection-app-catalogs")]
    public class AddSiteCollectionAppCatalog : PnPAdminCmdlet
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
            
            Tenant.GetSiteByUrl(url).RootWeb.TenantAppCatalog.SiteCollectionAppCatalogsSites.Add(url);
            ClientContext.ExecuteQueryRetry();
        }
    }
}