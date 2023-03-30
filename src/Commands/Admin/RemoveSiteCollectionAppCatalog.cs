using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteCollectionAppCatalog")]
    public class RemoveSiteCollectionAppCatalog : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            string url = null;
            Guid? id = null;
            if (Site.Site != null)
            {
                Site.Site.EnsureProperty(s => s.Url);
                url = Site.Site.Url;
            }
            else if (!string.IsNullOrEmpty(Site.Url))
            {
                url = Site.Url;
            }
            else if (Site.Id != Guid.Empty)
            {
                id = Site.Id;
            }

            if (!string.IsNullOrEmpty(url))
            {
                AdminContext.Web.TenantAppCatalog.SiteCollectionAppCatalogsSites.Remove(url);
                AdminContext.ExecuteQueryRetry();
            }
            else if (id != null)
            {
                AdminContext.Web.TenantAppCatalog.SiteCollectionAppCatalogsSites.RemoveById(id.Value);
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}