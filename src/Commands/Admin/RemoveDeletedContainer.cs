using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPDeletedContainer", SupportsShouldProcess = true)]
    public class RemoveDeletedContainer : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("ContainerId")]
        [ValidateNotNullOrWhiteSpace]
        public string Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        private HashSet<string> _deletedContainerIds;
        private bool _yesToAll;
        private bool _noToAll;

        protected override void ExecuteCmdlet()
        {
            // SharePoint Online rejects this by site url with "Deleting Deleted Container by ContainerSiteURL is not supported yet."
            var container = new ContainerPipeBind(Identity);
            if (container.Id == null)
            {
                WriteError(new ErrorRecord(new PSArgumentException("A deleted container cannot be permanently deleted by its site url. Use its id, as returned by Get-PnPDeletedContainer, or its api url.", nameof(Identity)), "PurgeBySiteUrlNotSupported", ErrorCategory.InvalidArgument, Identity));
                return;
            }

            // Only purge what is in the recycle bin, so the id of an active container, or a value that is no container id at all, which can bind from the pipeline, is never acted upon
            if (_deletedContainerIds == null)
            {
                var deletedContainers = Tenant.GetSPODeletedContainers();
                AdminContext.ExecuteQueryRetry();
                _deletedContainerIds = deletedContainers.Select(c => c.ContainerId).ToHashSet(StringComparer.Ordinal);
            }
            if (!_deletedContainerIds.Contains(container.Id))
            {
                WriteError(new ErrorRecord(new PSArgumentException($"Container '{container.Id}' is not in the recycle bin.", nameof(Identity)), "DeletedContainerNotFound", ErrorCategory.ObjectNotFound, container.Id));
                return;
            }

            if (!ShouldProcess(container.Id, "Permanently delete container from the recycle bin"))
            {
                return;
            }

            if (!Force && !ShouldContinue($"Permanently delete container '{container.Id}' and everything in it? It cannot be restored.", Properties.Resources.Confirm, ref _yesToAll, ref _noToAll))
            {
                return;
            }

            var purged = Tenant.PurgeSPODeletedContainerByContainerId(container.Id);
            AdminContext.ExecuteQueryRetry();
            if (!purged.Value)
            {
                WriteError(new ErrorRecord(new InvalidOperationException($"Container '{container.Id}' could not be permanently deleted, as the server did not report success."), "ContainerNotPurged", ErrorCategory.InvalidResult, container.Id));
                return;
            }
            _deletedContainerIds.Remove(container.Id);
        }
    }
}
