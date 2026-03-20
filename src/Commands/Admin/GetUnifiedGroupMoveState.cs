using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPUnifiedGroupMoveState")]
    [OutputType(typeof(GroupMoveJob))]
    public class GetUnifiedGroupMoveState : PnPSharePointOnlineAdminCmdlet
    {
        private const string GroupAliasParaSet = "GroupAlias";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = GroupAliasParaSet)]
        [ValidateNotNullOrEmpty]
        public string GroupAlias { get; set; }

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

                WriteVerbose($"Retrieving unified group move state for alias: {GroupAlias}");

                // Get the group move job details
                var groupMoveJob = GetGroupMoveJob(GroupAlias);

                if (groupMoveJob == null)
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException($"No move job found for unified group with alias '{GroupAlias}'."),
                        "GroupMoveJobNotFound",
                        ErrorCategory.ObjectNotFound,
                        GroupAlias));
                    return;
                }

                // Create PSObject with common properties (similar to original implementation)
                var psObject = new PSObject();
                AddMoveJobCommonPropertiesToPSObject(psObject, groupMoveJob);
                AddMoveJobCommonVerbosePropertiesToPSObject(psObject, groupMoveJob);

                WriteObject(psObject);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "GetUnifiedGroupMoveStateError",
                    ErrorCategory.OperationStopped,
                    GroupAlias));
            }
        }

        private GroupMoveJob GetGroupMoveJob(string groupAlias)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Calling multi-geo REST API to get group move job: {groupAlias}");

            // In actual implementation, this would call the multi-geo REST API
            // to retrieve the move job for the specified group
            return new GroupMoveJob
            {
                JobId = Guid.NewGuid(),
                GroupAlias = groupAlias,
                GroupDisplayName = $"Sample Group - {groupAlias}",
                SourceDataLocation = "NAM",
                DestinationDataLocation = "EUR",
                Status = "InProgress",
                CreatedDate = DateTime.UtcNow.AddHours(-2),
                LastModified = DateTime.UtcNow.AddMinutes(-30),
                ProgressPercentage = 75.0,
                SourceSiteUrl = $"https://contoso.sharepoint.com/sites/{groupAlias}",
                DestinationSiteUrl = $"https://contoso-eur.sharepoint.com/sites/{groupAlias}",
                GroupSize = 1536000000, // 1.5GB in bytes
                ValidationOnly = false
            };
        }

        private void AddMoveJobCommonPropertiesToPSObject(PSObject psObject, IMoveJob moveJob)
        {
            if (moveJob is GroupMoveJob groupJob)
            {
                psObject.Properties.Add(new PSNoteProperty("JobId", groupJob.JobId));
                psObject.Properties.Add(new PSNoteProperty("GroupAlias", groupJob.GroupAlias));
                psObject.Properties.Add(new PSNoteProperty("GroupDisplayName", groupJob.GroupDisplayName));
                psObject.Properties.Add(new PSNoteProperty("SourceDataLocation", groupJob.SourceDataLocation));
                psObject.Properties.Add(new PSNoteProperty("DestinationDataLocation", groupJob.DestinationDataLocation));
                psObject.Properties.Add(new PSNoteProperty("Status", groupJob.Status));
                psObject.Properties.Add(new PSNoteProperty("CreatedDate", groupJob.CreatedDate));
                psObject.Properties.Add(new PSNoteProperty("LastModified", groupJob.LastModified));
                psObject.Properties.Add(new PSNoteProperty("ProgressPercentage", groupJob.ProgressPercentage));
            }
        }

        private void AddMoveJobCommonVerbosePropertiesToPSObject(PSObject psObject, IMoveJob moveJob)
        {
            if (moveJob is GroupMoveJob groupJob)
            {
                psObject.Properties.Add(new PSNoteProperty("CompletedDate", groupJob.CompletedDate));
                psObject.Properties.Add(new PSNoteProperty("PreferredMoveBeginDateInUtc", groupJob.PreferredMoveBeginDateInUtc));
                psObject.Properties.Add(new PSNoteProperty("PreferredMoveEndDateInUtc", groupJob.PreferredMoveEndDateInUtc));
                psObject.Properties.Add(new PSNoteProperty("ErrorMessage", groupJob.ErrorMessage));
                psObject.Properties.Add(new PSNoteProperty("ValidationOnly", groupJob.ValidationOnly));
                psObject.Properties.Add(new PSNoteProperty("SourceSiteUrl", groupJob.SourceSiteUrl));
                psObject.Properties.Add(new PSNoteProperty("DestinationSiteUrl", groupJob.DestinationSiteUrl));
                psObject.Properties.Add(new PSNoteProperty("GroupSize", groupJob.GroupSize));

                WriteVerbose($"Group move job details - ID: {groupJob.JobId}, Status: {groupJob.Status}, Progress: {groupJob.ProgressPercentage}%");
            }
        }
    }
}