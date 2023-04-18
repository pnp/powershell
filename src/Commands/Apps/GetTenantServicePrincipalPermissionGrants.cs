using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantServicePrincipalPermissionGrants")]
    public class GetTenantServicePrincipalPermissionGrants : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
            var permissionGrants = servicePrincipal.PermissionGrants;
            AdminContext.Load(permissionGrants);
            AdminContext.ExecuteQueryRetry();
            WriteObject(permissionGrants.Select(g => new TenantServicePrincipalPermissionGrant(g)), true);
        }

    }
}