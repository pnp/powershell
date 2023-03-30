using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPHideDefaultThemes")]
    public class SetHideDefaultThemes : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public bool HideDefaultThemes = false;

        protected override void ExecuteCmdlet()
        {
            Tenant.HideDefaultThemes = HideDefaultThemes;
            AdminContext.ExecuteQueryRetry();
        }
    }
}