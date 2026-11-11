using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPContainerUser", SupportsShouldProcess = true)]
    public class SetContainerUser : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ContainerPipeBind Identity { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrWhiteSpace]
        public string LoginName { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public ContainerRole Role { get; set; }

        protected override void ExecuteCmdlet()
        {
            var containerId = Identity.GetContainerId(Tenant);
            if (containerId == null)
            {
                WriteError(new ErrorRecord(new PSArgumentException(Properties.Resources.ContainerNotFound, nameof(Identity)), "ContainerNotFound", ErrorCategory.ObjectNotFound, Identity.Url));
                return;
            }

            if (!ShouldProcess(containerId, $"Change the role of {LoginName} to {Role}"))
            {
                return;
            }

            // A user that already has a role, or does not have the role given, is reported for this container only, so the other containers in a pipeline are still processed
            try
            {
                Tenant.SetSPOContainerRole(containerId, LoginName, Role.ToString());
                AdminContext.ExecuteQueryRetry();
            }
            catch (ServerException ex)
            {
                WriteError(new ErrorRecord(ex, "SetContainerUserFailed", ErrorCategory.InvalidOperation, LoginName));
            }
        }
    }
}
