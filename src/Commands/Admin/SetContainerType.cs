using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPContainerType", SupportsShouldProcess = true)]
    [OutputType(typeof(SPContainerTypeProperties))]
    public class SetContainerType : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("ContainerTypeId")]
        public Guid Identity { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrWhiteSpace]
        public string ContainerTypeName { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrWhiteSpace]
        public string ApplicationRedirectUrl { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (Identity == Guid.Empty)
            {
                ThrowArgumentError($"-{nameof(Identity)} cannot be an empty GUID.", nameof(Identity));
            }

            if (!ParameterSpecified(nameof(ContainerTypeName)) && !ParameterSpecified(nameof(ApplicationRedirectUrl)))
            {
                ThrowArgumentError("Specify at least one setting to change.", null);
            }

            if (!ShouldProcess(Identity.ToString(), "Set container type"))
            {
                return;
            }

            // Properties left null are not changed
            var containerType = Tenant.SetSPOContainerType(new SPContainerTypeProperties
            {
                ContainerTypeId = Identity,
                DisplayName = ContainerTypeName,
                ApplicationRedirectUrl = ApplicationRedirectUrl
            });
            AdminContext.ExecuteQueryRetry();

            if (containerType.Value == null)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException(string.Format(Properties.Resources.ContainerTypeNotFound, Identity), nameof(Identity)), "ContainerTypeNotFound", ErrorCategory.ObjectNotFound, Identity));
            }
            WriteObject(containerType.Value);
        }

        private void ThrowArgumentError(string message, string parameterName)
        {
            ThrowTerminatingError(new ErrorRecord(new PSArgumentException(message, parameterName), "InvalidContainerTypeSettings", ErrorCategory.InvalidArgument, Identity));
        }
    }
}
