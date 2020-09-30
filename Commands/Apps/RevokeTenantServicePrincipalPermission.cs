using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPTenantServicePrincipalPermission")]
    public class RevokeTenantServicePrincipal : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ObjectId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("Revoke permission?","Continue"))
            {
                var servicePrincipal = new SPOWebAppServicePrincipal(ClientContext);
                var grant = servicePrincipal.PermissionGrants.GetByObjectId(ObjectId);
                grant.DeleteObject();
                ClientContext.ExecuteQuery();
            }
        }
    }
}