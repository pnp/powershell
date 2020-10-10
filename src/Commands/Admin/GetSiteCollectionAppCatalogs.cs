using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SiteCollectionAppCatalogs")]
    public class GetSiteCollectionAppCatalogs : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var allowedSites = this.SelectedWeb.TenantAppCatalog.SiteCollectionAppCatalogsSites;
            ClientContext.Load(allowedSites);
            ClientContext.ExecuteQueryRetry();

            WriteObject(allowedSites);
        }
    }
}