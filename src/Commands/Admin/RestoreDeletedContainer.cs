using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsData.Restore, "PnPDeletedContainer")]
    public class RestoreDeletedContainer : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            // SharePoint Online rejects restoring by site url with "Restore Deleted Container by ContainerSiteURL is not supported yet."
            var container = new ContainerPipeBind(Identity);
            if (container.Id == null)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException("A deleted container cannot be restored by its site url. Use its id, as returned by Get-PnPDeletedContainer, or its api url.", nameof(Identity)), "RestoreBySiteUrlNotSupported", ErrorCategory.InvalidArgument, Identity));
            }

            if (Force || ShouldContinue($"Restore container {Identity}?", Properties.Resources.Confirm))
            {
                LogDebug($"Restoring container {Identity}");
                var restored = Tenant.RestoreSPODeletedContainerByContainerId(container.Id);
                AdminContext.ExecuteQueryRetry();
                if (!restored.Value)
                {
                    ThrowTerminatingError(new ErrorRecord(new InvalidOperationException($"Container '{Identity}' could not be restored, as the server did not report success."), "ContainerNotRestored", ErrorCategory.InvalidResult, Identity));
                }
                LogDebug($"Restored container {Identity}");
            }
        }
    }
}
