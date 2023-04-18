using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantServicePrincipalPermissionRequests")]
    public class GetTenantServicePrincipalPermissionRequests : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
            var requests = servicePrincipal.PermissionRequests;
            AdminContext.Load(requests);
            AdminContext.ExecuteQueryRetry();
            WriteObject(requests, true);
        }

    }
}