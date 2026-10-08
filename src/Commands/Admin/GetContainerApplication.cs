using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainerApplication", DefaultParameterSetName = ParameterSet_All)]
    [OutputType(typeof(SPSyntexApplicationProperties))]
    public class GetContainerApplication : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_All = "All";
        private const string ParameterSet_ByOwningApplication = "By owning application";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ByOwningApplication)]
        public Guid OwningApplicationId { get; set; }

        [Parameter(Mandatory = false, Position = 1, ParameterSetName = ParameterSet_ByOwningApplication)]
        public Guid ApplicationId { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_All)
            {
                var applications = Tenant.GetSPOSyntexApplications();
                AdminContext.ExecuteQueryRetry();
                WriteObject(applications, true);
                return;
            }

            if (OwningApplicationId == Guid.Empty || (ParameterSpecified(nameof(ApplicationId)) && ApplicationId == Guid.Empty))
            {
                var parameterName = OwningApplicationId == Guid.Empty ? nameof(OwningApplicationId) : nameof(ApplicationId);
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException($"-{parameterName} cannot be an empty GUID.", parameterName), "EmptyApplicationId", ErrorCategory.InvalidArgument, null));
            }

            var application = Tenant.GetSPOSyntexConsumingApplications(OwningApplicationId, ApplicationId);
            AdminContext.ExecuteQueryRetry();
            if (application.Value == null)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException($"SharePoint Embedded application '{(ParameterSpecified(nameof(ApplicationId)) ? ApplicationId : OwningApplicationId)}' could not be found.", nameof(OwningApplicationId)), "ApplicationNotFound", ErrorCategory.ObjectNotFound, OwningApplicationId));
            }
            WriteObject(application.Value);
        }
    }
}
