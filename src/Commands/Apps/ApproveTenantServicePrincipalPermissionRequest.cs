using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Approve, "PnPTenantServicePrincipalPermissionRequest")]
    public class ApproveTenantServicePrincipalPermissionRequests : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid RequestId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue($"Approve request {RequestId}?", "Continue"))
            {
                var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
                var request = servicePrincipal.PermissionRequests.GetById(RequestId);
                var grant = request.Approve();
                AdminContext.Load(grant);
                AdminContext.ExecuteQueryRetry();
                WriteObject(new TenantServicePrincipalPermissionGrant(grant));
            }
        }

    }
}