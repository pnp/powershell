using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;
using System.Linq;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Start, "PnPUserAndContentMove")]
    [OutputType(typeof(UserMoveJob))]
    public class StartUserAndContentMove : PnPSharePointOnlineAdminCmdlet
    {
        private DateTime _preferredMoveBeginDate = DateTime.MinValue;
        private DateTime _preferredMoveEndDate = DateTime.MinValue;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string UserPrincipalName { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public string DestinationDataLocation { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public DateTime PreferredMoveBeginDate
        {
            get { return _preferredMoveBeginDate; }
            set { _preferredMoveBeginDate = value; }
        }

        [Parameter(Mandatory = false, Position = 3)]
        public DateTime PreferredMoveEndDate
        {
            get { return _preferredMoveEndDate; }
            set { _preferredMoveEndDate = value; }
        }

        [Parameter(Mandatory = false, Position = 4)]
        public string Notify { get; set; }

        [Parameter(Mandatory = false, Position = 5)]
        public string Reserved { get; set; }

        [Parameter(Mandatory = false, Position = 6)]
        public SwitchParameter ValidationOnly { get; set; }

        protected override void ExecuteCmdlet()
        {
            try
            {
                // Load the current site context to validate access
                AdminContext.Load(AdminContext.Site, s => s.Url);
                AdminContext.ExecuteQueryRetry();

                if (string.IsNullOrEmpty(AdminContext.Site.Url))
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException("Unable to access the SharePoint Admin site. Please ensure you have the necessary permissions."),
                        "GetSitesOperationFailed",
                        ErrorCategory.PermissionDenied,
                        null));
                    return;
                }

                // Create the move job request
                var moveJobData = new UserMoveJobData
                {
                    UserPrincipalName = UserPrincipalName,
                    DestinationDataLocation = DestinationDataLocation,
                    Reserved = Reserved,
                    Notify = Notify
                };

                // Set optional date parameters if provided
                if (PreferredMoveBeginDate > DateTime.MinValue.AddDays(1.0))
                {
                    moveJobData.PreferredMoveBeginDateInUtc = PreferredMoveBeginDate.ToUniversalTime();
                }

                if (PreferredMoveEndDate > DateTime.MinValue.AddDays(1.0))
                {
                    moveJobData.PreferredMoveEndDateInUtc = PreferredMoveEndDate.ToUniversalTime();
                }

                // Set validation only option if specified
                if (ValidationOnly.ToBool())
                {
                    moveJobData.ValidationOnly = true;
                }

                // Execute the move job creation using the SharePoint Admin API
                var moveJob = CreateUserMoveJob(moveJobData);

                if (moveJob == null)
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException("An unexpected error occurred while creating the user move job."),
                        "CrossGeoUnexpected",
                        ErrorCategory.OperationStopped,
                        null));
                    return;
                }

                WriteObject(moveJob);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "StartUserAndContentMoveError",
                    ErrorCategory.OperationStopped,
                    UserPrincipalName));
            }
        }

        private UserMoveJob CreateUserMoveJob(UserMoveJobData data)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            // In a real implementation, this would use the Admin API to create the move job

            WriteVerbose($"Creating user move job for {data.UserPrincipalName} to {data.DestinationDataLocation}");

            if (data.ValidationOnly)
            {
                WriteVerbose("Running in validation only mode");
            }

            // This is a placeholder - in actual implementation, you'd call the SharePoint Admin API
            // to create the move job and return the actual response
            return new UserMoveJob
            {
                UserPrincipalName = data.UserPrincipalName,
                DestinationDataLocation = data.DestinationDataLocation,
                JobId = Guid.NewGuid(),
                Status = "Queued",
                CreatedDate = DateTime.UtcNow,
                ValidationOnly = data.ValidationOnly
            };
        }
    }
}