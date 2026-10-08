using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPContainer", SupportsShouldProcess = true)]
    public class RemoveContainer : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ContainerPipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipRecycleBin { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ExecuteCmdlet()
        {
            var containerProperties = Identity.GetContainer(Tenant);
            if (containerProperties == null)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException(Properties.Resources.ContainerNotFound, nameof(Identity)), "ContainerNotFound", ErrorCategory.ObjectNotFound, Identity.Id ?? Identity.Url));
            }
            var containerId = containerProperties.ContainerId;

            if (!ShouldProcess(containerId, SkipRecycleBin ? "Permanently delete container" : "Move container to the recycle bin"))
            {
                return;
            }

            if (SkipRecycleBin && !Force && !ShouldContinue($"Permanently delete container '{containerId}' and everything in it? It cannot be restored.", Properties.Resources.Confirm))
            {
                return;
            }

            Tenant.RemoveSPOContainerByContainerId(containerId);
            AdminContext.ExecuteQueryRetry();

            if (SkipRecycleBin)
            {
                LogDebug($"Purging container {containerId} from the recycle bin");
                string failure = null;
                try
                {
                    var purged = Tenant.PurgeSPODeletedContainerByContainerId(containerId);
                    AdminContext.ExecuteQueryRetry();
                    if (!purged.Value)
                    {
                        failure = "the server did not report success";
                    }
                }
                catch (Exception ex) when (ex is not PipelineStoppedException)
                {
                    failure = ex.Message;
                }

                if (failure != null)
                {
                    ThrowTerminatingError(new ErrorRecord(new InvalidOperationException($"Container '{containerId}' was moved to the recycle bin, but could not be permanently deleted: {failure}. It can still be restored with Restore-PnPDeletedContainer."), "ContainerNotPurged", ErrorCategory.InvalidResult, containerId));
                }
            }
        }
    }
}
