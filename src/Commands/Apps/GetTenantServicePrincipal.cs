using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantServicePrincipal")]
    public class GetTenantServicePrincipal : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
            AdminContext.Load(servicePrincipal);
            AdminContext.ExecuteQueryRetry();
            WriteObject(servicePrincipal);
        }
    }
}