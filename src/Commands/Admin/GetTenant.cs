using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenant")]
    public class GetTenant : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant);
            AdminContext.Load(Tenant, t => t.HideDefaultThemes);
            AdminContext.ExecuteQueryRetry();
            WriteObject(new SPOTenant(Tenant, AdminContext));
        }
    }
}
