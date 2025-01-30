using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInstance")]
    public class GetTenantInstance : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
           var instances = Tenant.GetTenantInstances();
           AdminContext.Load(instances);
           AdminContext.ExecuteQueryRetry();
           WriteObject(instances.Select(i => Model.TenantInstance.Convert(i)));
        }
    }
}