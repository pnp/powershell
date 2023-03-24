using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTemporarilyDisableAppBar")]
    public class GetTemporarilyDisableAppBar : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant, t => t.IsAppBarTemporarilyDisabled);
            AdminContext.ExecuteQueryRetry();
            WriteObject(Tenant.IsAppBarTemporarilyDisabled);
        }
    }
}