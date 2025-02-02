using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantAppCatalogUrl")]
    public class SetTenantAppCatalogUrl : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        protected override void ExecuteCmdlet()
        {
            var settings = TenantSettings.GetCurrent(AdminContext);
            settings.SetCorporateCatalog(Url);
            AdminContext.ExecuteQueryRetry();
        }
    }
}