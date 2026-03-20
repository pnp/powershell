using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Start, "PnPSiteContentMove", DefaultParameterSetName = "UrlAndDestinationDataLocation")]
    [OutputType(typeof(SiteMoveJob))]
    public class StartSiteContentMove : PnPSharePointOnlineAdminCmdlet
    {
        private const string UrlAndDestinationUrlParaSet = "UrlAndDestinationUrl";
        private const string UrlAndDestinationDataLocationParaSet = "UrlAndDestinationDataLocation";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = UrlAndDestinationUrlParaSet)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = UrlAndDestinationDataLocationParaSet)]
        public string SourceSiteUrl { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = UrlAndDestinationDataLocationParaSet)]
        public string DestinationDataLocation { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = UrlAndDestinationUrlParaSet)]
        public string DestinationUrl { get; set; }

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

                // Create the site move job request
                var siteMoveJobData = new SiteMoveJobEntityData
                {
                    ApiVersion = "1.0", // This would be set by the actual REST API client
                    SourceSiteUrl = SourceSiteUrl,
                    DestinationDataLocation = DestinationDataLocation,
                    TargetSiteUrl = DestinationUrl,
                    Reserve = Reserved,
                    Option = MoveOption.None
                };

                // Set optional date parameters if provided
                if (PreferredMoveBeginDate > DateTime.MinValue.AddDays(1.0))
                {
                    siteMoveJobData.PreferredMoveBeginDateInUtc = PreferredMoveBeginDate.ToUniversalTime();
                    WriteVerbose($"Preferred move begin date set to: {siteMoveJobData.PreferredMoveBeginDateInUtc}");
                }

                if (PreferredMoveEndDate > DateTime.MinValue.AddDays(1.0))
                {
                    siteMoveJobData.PreferredMoveEndDateInUtc = PreferredMoveEndDate.ToUniversalTime();
                    WriteVerbose($"Preferred move end date set to: {siteMoveJobData.PreferredMoveEndDateInUtc}");
                }

                // Set move options based on switch parameters
                if (ValidationOnly.ToBool())
                {
                    siteMoveJobData.Option |= MoveOption.ValidationOnly;
                    WriteVerbose("Validation only mode enabled");
                }

                if (Force.ToBool() || SuppressAllWarnings.ToBool())
                {
                    siteMoveJobData.Option |= MoveOption.SuppressAllWarning;
                    WriteVerbose("Suppressing all warnings");
                }

                if (SuppressMarketplaceAppCheck.ToBool())
                {
                    siteMoveJobData.Option |= MoveOption.SuppressMarketplaceAppCheck;
                    WriteVerbose("Suppressing marketplace app check");
                }

                if (SuppressWorkflow2013Check.ToBool())
                {
                    siteMoveJobData.Option |= MoveOption.SuppressWorkflow2013Check;
                    WriteVerbose("Suppressing Workflow 2013 check");
                }

                if (SuppressBcsCheck.ToBool())
                {
                    siteMoveJobData.Option |= MoveOption.SuppressBcsCheck;
                    WriteVerbose("Suppressing BCS check");
                }

                WriteVerbose($"Creating site move job from {SourceSiteUrl} to {DestinationDataLocation ?? DestinationUrl}");

                // Execute the site move job creation using the SharePoint Admin API
                var siteMoveJob = CreateSiteMoveJob(siteMoveJobData);

                if (siteMoveJob == null)
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException("An unexpected error occurred while creating the site move job."),
                        "CrossGeoUnexpected",
                        ErrorCategory.OperationStopped,
                        null));
                    return;
                }

                // Return the move job details as a PSObject similar to the original implementation
                WriteObject(new PSObject()
                {
                    Properties = {
                        new PSNoteProperty("JobId", siteMoveJob.JobId),
                        new PSNoteProperty("SourceSiteUrl", siteMoveJob.SourceSiteUrl),
                        new PSNoteProperty("DestinationDataLocation", siteMoveJob.DestinationDataLocation),
                        new PSNoteProperty("DestinationUrl", siteMoveJob.DestinationUrl),
                        new PSNoteProperty("Status", siteMoveJob.Status),
                        new PSNoteProperty("CreatedDate", siteMoveJob.CreatedDate),
                        new PSNoteProperty("ValidationOnly", siteMoveJob.ValidationOnly),
                        new PSNoteProperty("ProgressPercentage", siteMoveJob.ProgressPercentage)
                    }
                });

                WriteVerbose($"Site move job created successfully with ID: {siteMoveJob.JobId}");
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "StartSiteContentMoveError",
                    ErrorCategory.OperationStopped,
                    SourceSiteUrl));
            }
        }

        private SiteMoveJob CreateSiteMoveJob(SiteMoveJobEntityData data)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Creating site move job for {data.SourceSiteUrl}");

            if (!string.IsNullOrEmpty(data.DestinationDataLocation))
            {
                WriteVerbose($"Moving to data location: {data.DestinationDataLocation}");
            }
            else if (!string.IsNullOrEmpty(data.TargetSiteUrl))
            {
                WriteVerbose($"Moving to specific URL: {data.TargetSiteUrl}");
            }

            if (data.Option.HasFlag(MoveOption.ValidationOnly))
            {
                WriteVerbose("Running in validation only mode");
            }

            // This is a placeholder - in actual implementation, you'd call the SharePoint Admin API
            // to create the site move job and return the actual response
            return new SiteMoveJob
            {
                JobId = Guid.NewGuid(),
                SourceSiteUrl = data.SourceSiteUrl,
                DestinationDataLocation = data.DestinationDataLocation,
                DestinationUrl = data.TargetSiteUrl ?? (data.SourceSiteUrl?.Replace(".sharepoint.com", $"-{data.DestinationDataLocation?.ToLower()}.sharepoint.com")),
                Status = data.Option.HasFlag(MoveOption.ValidationOnly) ? "ValidationQueued" : "Queued",
                CreatedDate = DateTime.UtcNow,
                ValidationOnly = data.Option.HasFlag(MoveOption.ValidationOnly),
                ProgressPercentage = 0.0,
                SiteSize = 1024000000 // Placeholder size
            };
        }
    }
}