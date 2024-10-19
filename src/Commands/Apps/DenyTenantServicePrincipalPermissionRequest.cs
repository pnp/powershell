using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Deny, "PnPTenantServicePrincipalPermissionRequest")]
    public class DenyTenantServicePrincipalPermissionRequests : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid RequestId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue($"Deny request {RequestId}?", Properties.Resources.Confirm))
            {
                var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
                var request = servicePrincipal.PermissionRequests.GetById(RequestId);
                request.Deny();
                AdminContext.ExecuteQueryRetry();
            }
        }

    }
}