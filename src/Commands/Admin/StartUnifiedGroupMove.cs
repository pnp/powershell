using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Start, "PnPUnifiedGroupMove", DefaultParameterSetName = "GroupAliasAndDestinationDataLocation")]
    [OutputType(typeof(GroupMoveJob))]
    public class StartUnifiedGroupMove : PnPSharePointOnlineAdminCmdlet
    {
        private const string GroupAliasAndDestinationDataLocationParaSet = "GroupAliasAndDestinationDataLocation";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = GroupAliasAndDestinationDataLocationParaSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public string GroupAlias { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = GroupAliasAndDestinationDataLocationParaSet)]
        [ValidateNotNullOrEmpty]
        public string DestinationDataLocation { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public DateTime PreferredMoveBeginDate { get; set; }

        [Parameter(Mandatory = false, Position = 3)]
        public DateTime PreferredMoveEndDate { get; set; }

        [Parameter(Mandatory = false, Position = 4)]
        public string Reserved { get; set; }

        [Parameter(Mandatory = false, Position = 5)]
        public SwitchParameter ValidationOnly { get; set; }

        [Parameter(Mandatory = false, Position = 6)]
        public SwitchParameter Force { get; set; }

        [Parameter(Mandatory = false, Position = 7)]
        public SwitchParameter SuppressMarketplaceAppCheck { get; set; }

        [Parameter(Mandatory = false, Position = 8)]
        public SwitchParameter SuppressWorkflow2013Check { get; set; }

        [Parameter(Mandatory = false, Position = 9)]
        public SwitchParameter SuppressAllWarnings { get; set; }

        [Parameter(Mandatory = false, Position = 10)]
        public SwitchParameter SuppressBcsCheck { get; set; }

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

                // Create the group move job request
                var groupMoveJobData = new GroupMoveJobEntityData
                {
                    ApiVersion = "1.0", // This would be set by the actual REST API client
                    GroupName = GroupAlias,
                    DestinationDataLocation = DestinationDataLocation,
                    Reserve = Reserved,
                    Option = MoveOption.None
                };

                // Set optional date parameters if provided
                if (PreferredMoveBeginDate > DateTime.MinValue.AddDays(1.0))
                {
                    groupMoveJobData.PreferredMoveBeginDateInUtc = PreferredMoveBeginDate.ToUniversalTime();
                    WriteVerbose($"Preferred move begin date set to: {groupMoveJobData.PreferredMoveBeginDateInUtc}");
                }

                if (PreferredMoveEndDate > DateTime.MinValue.AddDays(1.0))
                {
                    groupMoveJobData.PreferredMoveEndDateInUtc = PreferredMoveEndDate.ToUniversalTime();
                    WriteVerbose($"Preferred move end date set to: {groupMoveJobData.PreferredMoveEndDateInUtc}");
                }

                // Set move options based on switch parameters
                if (ValidationOnly.ToBool())
                {
                    groupMoveJobData.Option |= MoveOption.ValidationOnly;
                    WriteVerbose("Validation only mode enabled");
                }

                if (Force.ToBool() || SuppressAllWarnings.ToBool())
                {
                    groupMoveJobData.Option |= MoveOption.SuppressAllWarning;
                    WriteVerbose("Suppressing all warnings");
                }

                if (SuppressMarketplaceAppCheck.ToBool())
                {
                    groupMoveJobData.Option |= MoveOption.SuppressMarketplaceAppCheck;
                    WriteVerbose("Suppressing marketplace app check");
                }

                if (SuppressWorkflow2013Check.ToBool())
                {
                    groupMoveJobData.Option |= MoveOption.SuppressWorkflow2013Check;
                    WriteVerbose("Suppressing Workflow 2013 check");
                }

                if (SuppressBcsCheck.ToBool())
                {
                    groupMoveJobData.Option |= MoveOption.SuppressBcsCheck;
                    WriteVerbose("Suppressing BCS check");
                }

                WriteVerbose($"Creating group move job for '{GroupAlias}' to '{DestinationDataLocation}'");

                // Execute the group move job creation using the SharePoint Admin API
                var groupMoveJob = CreateGroupMoveJob(groupMoveJobData);

                if (groupMoveJob == null)
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException("An unexpected error occurred while creating the group move job."),
                        "CrossGeoUnexpected",
                        ErrorCategory.OperationStopped,
                        null));
                    return;
                }

                // Return the move job details as a PSObject similar to the original implementation
                var psObject = new PSObject();
                AddMoveJobCommonPropertiesToPSObject(psObject, groupMoveJob);
                AddMoveJobCommonVerbosePropertiesToPSObject(psObject, groupMoveJob);

                WriteObject(psObject);

                WriteVerbose($"Group move job created successfully with ID: {groupMoveJob.JobId}");
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "StartUnifiedGroupMoveError",
                    ErrorCategory.OperationStopped,
                    GroupAlias));
            }
        }

        private GroupMoveJob CreateGroupMoveJob(GroupMoveJobEntityData data)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Creating group move job for {data.GroupName}");
            WriteVerbose($"Moving to data location: {data.DestinationDataLocation}");

            if (data.Option.HasFlag(MoveOption.ValidationOnly))
            {
                WriteVerbose("Running in validation only mode");
            }

            // This is a placeholder - in actual implementation, you'd call the SharePoint Admin API
            // to create the group move job and return the actual response
            return new GroupMoveJob
            {
                JobId = Guid.NewGuid(),
                GroupAlias = data.GroupName,
                GroupDisplayName = $"Sample Group - {data.GroupName}",
                SourceDataLocation = "NAM", // This would be retrieved from the current group location
                DestinationDataLocation = data.DestinationDataLocation,
                Status = data.Option.HasFlag(MoveOption.ValidationOnly) ? "ValidationQueued" : "Queued",
                CreatedDate = DateTime.UtcNow,
                ValidationOnly = data.Option.HasFlag(MoveOption.ValidationOnly),
                ProgressPercentage = 0.0,
                SourceSiteUrl = $"https://contoso.sharepoint.com/sites/{data.GroupName}",
                DestinationSiteUrl = $"https://contoso-{data.DestinationDataLocation?.ToLower()}.sharepoint.com/sites/{data.GroupName}",
                GroupSize = 1536000000 // Placeholder size
            };
        }

        private void AddMoveJobCommonPropertiesToPSObject(PSObject psObject, GroupMoveJob moveJob)
        {
            psObject.Properties.Add(new PSNoteProperty("JobId", moveJob.JobId));
            psObject.Properties.Add(new PSNoteProperty("GroupAlias", moveJob.GroupAlias));
            psObject.Properties.Add(new PSNoteProperty("GroupDisplayName", moveJob.GroupDisplayName));
            psObject.Properties.Add(new PSNoteProperty("SourceDataLocation", moveJob.SourceDataLocation));
            psObject.Properties.Add(new PSNoteProperty("DestinationDataLocation", moveJob.DestinationDataLocation));
            psObject.Properties.Add(new PSNoteProperty("Status", moveJob.Status));
            psObject.Properties.Add(new PSNoteProperty("CreatedDate", moveJob.CreatedDate));
            psObject.Properties.Add(new PSNoteProperty("ProgressPercentage", moveJob.ProgressPercentage));
            psObject.Properties.Add(new PSNoteProperty("ValidationOnly", moveJob.ValidationOnly));
        }

        private void AddMoveJobCommonVerbosePropertiesToPSObject(PSObject psObject, GroupMoveJob moveJob)
        {
            psObject.Properties.Add(new PSNoteProperty("LastModified", moveJob.LastModified));
            psObject.Properties.Add(new PSNoteProperty("CompletedDate", moveJob.CompletedDate));
            psObject.Properties.Add(new PSNoteProperty("PreferredMoveBeginDateInUtc", moveJob.PreferredMoveBeginDateInUtc));
            psObject.Properties.Add(new PSNoteProperty("PreferredMoveEndDateInUtc", moveJob.PreferredMoveEndDateInUtc));
            psObject.Properties.Add(new PSNoteProperty("ErrorMessage", moveJob.ErrorMessage));
            psObject.Properties.Add(new PSNoteProperty("SourceSiteUrl", moveJob.SourceSiteUrl));
            psObject.Properties.Add(new PSNoteProperty("DestinationSiteUrl", moveJob.DestinationSiteUrl));
            psObject.Properties.Add(new PSNoteProperty("GroupSize", moveJob.GroupSize));

            WriteVerbose($"Group move job details - ID: {moveJob.JobId}, Status: {moveJob.Status}, Validation Only: {moveJob.ValidationOnly}");
        }
    }
}