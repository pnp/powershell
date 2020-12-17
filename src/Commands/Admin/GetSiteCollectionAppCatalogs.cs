using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteCollectionAppCatalogs")]
    public class GetSiteCollectionAppCatalogs : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var allowedSites = this.CurrentWeb.TenantAppCatalog.SiteCollectionAppCatalogsSites;
            ClientContext.Load(allowedSites);
            ClientContext.ExecuteQueryRetry();

            WriteObject(allowedSites);
        }
    }
}