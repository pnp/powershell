using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInstance")]
    public class GetTenantInstance : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
           var instances = Tenant.GetTenantInstances();
           ClientContext.Load(instances);
           ClientContext.ExecuteQueryRetry();
           WriteObject(instances.Select(i => Model.TenantInstance.Convert(i)));
        }
    }
}