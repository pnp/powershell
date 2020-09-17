using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantCdnOrigin")]
    [CmdletHelp("Returns the current registered origins from the public or private content delivery network (CDN).",
        DetailedDescription = @"Returns the current registered origins from the public or private content delivery network (CDN).",
        Category = CmdletHelpCategory.TenantAdmin)]
    [CmdletExample(
        Code = @"PS:> Get-PnPTenantCdnOrigin -CdnType Public",
        Remarks = @"Returns the configured CDN origins for the specified CDN type", SortOrder = 1)]
    public class GetTenantCdnOrigin : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The type of cdn to retrieve the origins from")]
        public SPOTenantCdnType CdnType;

        protected override void ExecuteCmdlet()
        {
            var origins = Tenant.GetTenantCdnOrigins(CdnType);
            ClientContext.ExecuteQueryRetry();
            WriteObject(origins, true);
        }
    }
}