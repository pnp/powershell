using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph.FileStorage;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.New, "PnPContainer", SupportsShouldProcess = true)]
    [OutputType(typeof(FileStorageContainer))]
    [RequiredApiDelegatedOrApplicationPermissions("graph/FileStorageContainer.Selected")]
    public class NewContainer : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("DisplayName")]
        [ValidateNotNullOrWhiteSpace]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        public Guid ContainerTypeId { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public bool OcrEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool ItemVersioningEnabled { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateRange(1, int.MaxValue)]
        public int ItemMajorVersionLimit { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Activate { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ContainerTypeId == Guid.Empty)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException($"-{nameof(ContainerTypeId)} cannot be an empty GUID.", nameof(ContainerTypeId)), "EmptyContainerTypeId", ErrorCategory.InvalidArgument, null));
            }

            var request = new FileStorageContainer
            {
                DisplayName = Name,
                Description = Description,
                ContainerTypeId = ContainerTypeId
            };

            if (ParameterSpecified(nameof(OcrEnabled)) || ParameterSpecified(nameof(ItemVersioningEnabled)) || ParameterSpecified(nameof(ItemMajorVersionLimit)))
            {
                request.Settings = new FileStorageContainerSettings
                {
                    IsOcrEnabled = ParameterSpecified(nameof(OcrEnabled)) ? OcrEnabled : null,
                    IsItemVersioningEnabled = ParameterSpecified(nameof(ItemVersioningEnabled)) ? ItemVersioningEnabled : null,
                    ItemMajorVersionLimit = ParameterSpecified(nameof(ItemMajorVersionLimit)) ? ItemMajorVersionLimit : null
                };
            }

            if (!ShouldProcess(Name, $"Create {(Activate ? "and activate " : string.Empty)}container of container type {ContainerTypeId}"))
            {
                return;
            }

            var container = GraphRequestHelper.Post("v1.0/storage/fileStorage/containers", request);
            if (container?.Id == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException($"Container '{Name}' was not created, as Microsoft Graph returned no container."), "ContainerNotCreated", ErrorCategory.InvalidResult, Name));
            }

            if (!Activate)
            {
                LogWarning($"Container '{Name}' has been created inactive. Activate it within 24 hours with Enable-PnPContainer, or add content to it, or it will be deleted automatically.");
                WriteObject(container);
                return;
            }

            // The container exists from here on, so it is always returned, also when activating it fails, to prevent a retry from creating a second one
            Exception activationFailure = null;
            try
            {
                GraphRequestHelper.PostHttpContent($"v1.0/storage/fileStorage/containers/{container.Id}/activate", null);
                container.Status = "active";
            }
            catch (Exception ex) when (ex is not PipelineStoppedException)
            {
                activationFailure = ex;
            }

            WriteObject(container);
            if (activationFailure != null)
            {
                WriteError(new ErrorRecord(new InvalidOperationException($"Container '{Name}' ({container.Id}) has been created, but could not be activated: {activationFailure.Message} Activate it within 24 hours with Enable-PnPContainer, or it will be deleted automatically.", activationFailure), "ContainerNotActivated", ErrorCategory.InvalidResult, container));
            }
        }
    }
}
