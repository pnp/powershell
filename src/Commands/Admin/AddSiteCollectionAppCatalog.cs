using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteCollectionAppCatalog")]
    public class AddSiteCollectionAppCatalog : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = false, Position = 0)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            string url = null;
            if (ParameterSpecified(nameof(Site)))
            {
                if (Site.Site != null)
                {
                    Site.Site.EnsureProperty(s => s.Url);
                    url = Site.Site.Url;
                }
                else if (!string.IsNullOrEmpty(Site.Url))
                {
                    url = Site.Url.TrimEnd('/');
                }
            }
            else
            {
                url = Connection.Url;
            }

            Tenant.GetSiteByUrl(url).RootWeb.TenantAppCatalog.SiteCollectionAppCatalogsSites.Add(url);
            AdminContext.ExecuteQueryRetry();
        }
    }
}