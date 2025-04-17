using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPStorageEntity")]
    
    public class RemovePnPStorageEntity : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Key;

        [Parameter(Mandatory = false)]
        public StorageEntityScope Scope = StorageEntityScope.Tenant;

        protected override void ExecuteCmdlet()
        {
            if (Scope == StorageEntityScope.Tenant)
            {
                var appCatalogUri = ClientContext.Web.GetAppCatalog();
                if(appCatalogUri != null)
                {
                    using (var clonedContext = ClientContext.Clone(appCatalogUri))
                    {
                        clonedContext.Web.RemoveStorageEntity(Key);
                        clonedContext.ExecuteQueryRetry();
                    }
                }
                else
                {
                    LogWarning("Tenant app catalog is not available on this tenant.");
                }                
            }
            else
            {
                var appcatalog = ClientContext.Site.RootWeb.SiteCollectionAppCatalog;
                ClientContext.Load(appcatalog);
                ClientContext.ExecuteQueryRetry();
                if (appcatalog.ServerObjectIsNull == false)
                {
                    ClientContext.Site.RootWeb.RemoveStorageEntity(Key);
                    ClientContext.ExecuteQueryRetry();
                }
                else
                {
                    LogWarning("Site Collection App Catalog is not available on this site.");
                }
            }
        }
    }
}