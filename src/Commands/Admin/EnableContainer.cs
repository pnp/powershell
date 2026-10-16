using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPContainer", SupportsShouldProcess = true)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/FileStorageContainer.Selected")]
    public class EnableContainer : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ContainerPipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            // Microsoft Graph addresses containers by id only
            if (Identity.Id == null)
            {
                WriteError(new ErrorRecord(new PSArgumentException("Specify the container by its id or its api url, as Microsoft Graph cannot look a container up by its site url.", nameof(Identity)), "ContainerSiteUrlNotSupported", ErrorCategory.InvalidArgument, Identity.Url));
                return;
            }

            if (!ShouldProcess(Identity.Id, "Activate container"))
            {
                return;
            }

            GraphRequestHelper.PostHttpContent($"v1.0/storage/fileStorage/containers/{Identity.Id}/activate", null);
        }
    }
}
