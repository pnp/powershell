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
            ClientContext.Load(Tenant, t => t.IsAppBarTemporarilyDisabled);
            ClientContext.ExecuteQueryRetry();
            WriteObject(Tenant.IsAppBarTemporarilyDisabled);
        }
    }
}