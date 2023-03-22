using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTemporarilyDisableAppBar")]
    public class SetTemporarilyDisableAppBar : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public bool Enabled;

        protected override void ExecuteCmdlet()
        {
            Tenant.IsAppBarTemporarilyDisabled = Enabled;
            AdminContext.ExecuteQueryRetry();
        }
    }
}