using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Approve, "PnPTenantServicePrincipalPermissionRequest")]
    public class ApproveTenantServicePrincipalPermissionRequests : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public GuidPipeBind RequestId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue($"Approve request {RequestId.Id}?", "Continue"))
            {
                var servicePrincipal = new SPOWebAppServicePrincipal(ClientContext);
                var request = servicePrincipal.PermissionRequests.GetById(RequestId.Id);
                var grant = request.Approve();
                ClientContext.Load(grant);
                ClientContext.ExecuteQueryRetry();
                WriteObject(new TenantServicePrincipalPermissionGrant(grant));
            }
        }

    }
}