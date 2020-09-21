using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantAppCatalogUrl", SupportsShouldProcess = true)]
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