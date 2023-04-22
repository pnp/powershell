using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantAppCatalogUrl")]
    public class GetTenantAppCatalogUrl : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var settings = TenantSettings.GetCurrent(ClientContext);
            settings.EnsureProperties(s => s.CorporateCatalogUrl);
            WriteObject(settings.CorporateCatalogUrl);
        }
    }
}