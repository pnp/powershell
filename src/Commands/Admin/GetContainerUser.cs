using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainerUser")]
    [OutputType(typeof(SPContainerUser))]
    public class GetContainerUser : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ContainerPipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            var container = Identity.GetContainer(Tenant, refresh: true);
            if (container == null)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException(Properties.Resources.ContainerNotFound, nameof(Identity)), "ContainerNotFound", ErrorCategory.ObjectNotFound, Identity.Id ?? Identity.Url));
            }

            WriteUsers(container.ContainerId, container.Owners, ContainerRole.Owner);
            WriteUsers(container.ContainerId, container.Managers, ContainerRole.Manager);
            WriteUsers(container.ContainerId, container.Writers, ContainerRole.Writer);
            WriteUsers(container.ContainerId, container.Readers, ContainerRole.Reader);
        }

        private void WriteUsers(string containerId, IList<string> loginNames, ContainerRole role)
        {
            if (loginNames == null)
            {
                return;
            }

            foreach (var loginName in loginNames)
            {
                WriteObject(new SPContainerUser(containerId, loginName, role));
            }
        }
    }
}
