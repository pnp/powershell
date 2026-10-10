using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPContainerApplicationPermission", SupportsShouldProcess = true)]
    [OutputType(typeof(FileStorageContainerTypeAppPermissionGrant))]
    [RequiredApiDelegatedOrApplicationPermissions("graph/FileStorageContainerTypeReg.Selected")]
    [RequiredApiDelegatedPermissions("graph/FileStorageContainerTypeReg.Manage.All")]
    public class SetContainerApplicationPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Guid ContainerTypeId { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        [Alias("GuestApplicationId")]
        public Guid ApplicationId { get; set; }

        [Parameter(Mandatory = false)]
        [Alias("PermissionAppOnly")]
        [ValidateNotNullOrEmpty]
        public ContainerApplicationPermission[] AppOnlyPermissions { get; set; }

        [Parameter(Mandatory = false)]
        [Alias("PermissionDelegated")]
        [ValidateNotNullOrEmpty]
        public ContainerApplicationPermission[] DelegatedPermissions { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ContainerTypeId == Guid.Empty || ApplicationId == Guid.Empty)
            {
                var parameterName = ContainerTypeId == Guid.Empty ? nameof(ContainerTypeId) : nameof(ApplicationId);
                ThrowArgumentError($"-{parameterName} cannot be an empty GUID.", parameterName);
            }

            if (!ParameterSpecified(nameof(AppOnlyPermissions)) && !ParameterSpecified(nameof(DelegatedPermissions)))
            {
                ThrowArgumentError($"Specify -{nameof(AppOnlyPermissions)}, -{nameof(DelegatedPermissions)} or both.", null);
            }

            ValidateNoneIsAlone(AppOnlyPermissions, nameof(AppOnlyPermissions));
            ValidateNoneIsAlone(DelegatedPermissions, nameof(DelegatedPermissions));

            var grantsUrl = $"v1.0/storage/fileStorage/containerTypeRegistrations/{ContainerTypeId}/applicationPermissionGrants";
            var existing = GraphRequestHelper.GetResultCollection<FileStorageContainerTypeAppPermissionGrant>(grantsUrl)
                .FirstOrDefault(g => string.Equals(g.AppId, ApplicationId.ToString(), StringComparison.OrdinalIgnoreCase));

            if (!ShouldProcess(ApplicationId.ToString(), $"Set the permissions of the application on the containers of container type {ContainerTypeId}"))
            {
                return;
            }

            // The server resets a list left out of the request to none, for a PATCH as well as a PUT, so both lists are always sent: the one passed in, and the
            // other one as the application has it now, or none for an application without a grant yet. Only the permissions passed in change that way.
            var grant = new FileStorageContainerTypeAppPermissionGrant
            {
                ApplicationPermissions = AppOnlyPermissions != null ? ToGraphValues(AppOnlyPermissions) : existing?.ApplicationPermissions ?? ToGraphValues([ContainerApplicationPermission.None]),
                DelegatedPermissions = DelegatedPermissions != null ? ToGraphValues(DelegatedPermissions) : existing?.DelegatedPermissions ?? ToGraphValues([ContainerApplicationPermission.None])
            };
            WriteObject(GraphRequestHelper.Put($"{grantsUrl}/{ApplicationId}", grant));
        }

        private void ValidateNoneIsAlone(ContainerApplicationPermission[] permissions, string parameterName)
        {
            if (permissions != null && permissions.Length > 1 && permissions.Contains(ContainerApplicationPermission.None))
            {
                ThrowArgumentError($"-{parameterName} {ContainerApplicationPermission.None} cannot be combined with other permissions.", parameterName);
            }
        }

        private static List<string> ToGraphValues(ContainerApplicationPermission[] permissions)
        {
            return permissions.Distinct().Select(p => JsonNamingPolicy.CamelCase.ConvertName(p.ToString())).ToList();
        }

        private void ThrowArgumentError(string message, string parameterName)
        {
            ThrowTerminatingError(new ErrorRecord(new PSArgumentException(message, parameterName), "InvalidContainerApplicationPermission", ErrorCategory.InvalidArgument, ApplicationId));
        }
    }
}
