using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPHideDefaultThemes")]
    public class SetHideDefaultThemes : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool HideDefaultThemes = false;

        protected override void ExecuteCmdlet()
        {
            Tenant.HideDefaultThemes = HideDefaultThemes;
            ClientContext.ExecuteQueryRetry();
        }
    }
}