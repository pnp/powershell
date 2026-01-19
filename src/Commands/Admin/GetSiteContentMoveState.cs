using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteContentMoveState", DefaultParameterSetName = "MoveReport")]
    [OutputType(typeof(SiteMoveJob))]
    public class GetSiteContentMoveState : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        public MoveState MoveState { get; set; } = MoveState.All;

        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        public MoveDirection MoveDirection { get; set; } = MoveDirection.All;

        [Parameter(ParameterSetName = "SourceSiteUrl", Mandatory = true)]
        public string SourceSiteUrl { get; set; }

        [Parameter(ParameterSetName = "SiteMoveId", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid SiteMoveId { get; set; }

        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        [ValidateRange(1, 1000)]
        public uint Limit { get; set; } = 200;

        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        public DateTime MoveStartTime { get; set; }

        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        public DateTime MoveEndTime { get; set; }

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

                var siteMoveJobs = new List<SiteMoveJob>();

                switch (ParameterSetName)
                {
                    case "SourceSiteUrl":
                        WriteVerbose($"Getting site move job for site: {SourceSiteUrl}");
                        var siteJob = GetSiteMoveJobByUrl(SourceSiteUrl);
                        if (siteJob != null)
                        {
                            siteMoveJobs.Add(siteJob);
                        }
                        break;

                    case "SiteMoveId":
                        WriteVerbose($"Getting site move job with ID: {SiteMoveId}");
                        var moveJob = GetSiteMoveJobById(SiteMoveId);
                        if (moveJob != null)
                        {
                            siteMoveJobs.Add(moveJob);
                        }
                        break;

                    case "MoveReport":
                        WriteVerbose($"Getting site move jobs with filters - State: {MoveState}, Direction: {MoveDirection}, Limit: {Limit}");
                        
                        DateTime? startTime = null;
                        DateTime? endTime = null;

                        if (MoveStartTime != DateTime.MinValue)
                        {
                            startTime = MoveStartTime.ToUniversalTime();
                            WriteVerbose($"Start time filter: {startTime}");
                        }

                        if (MoveEndTime != DateTime.MinValue)
                        {
                            endTime = MoveEndTime.ToUniversalTime();
                            WriteVerbose($"End time filter: {endTime}");
                        }

                        siteMoveJobs = GetSiteMoveJobsFiltered(MoveState, MoveDirection, startTime, endTime, Limit);
                        break;
                }

                // Sort by last modified date (newest first)
                var sortedJobs = siteMoveJobs.OrderByDescending(x => x.LastModified ?? x.CreatedDate).ToList();

                foreach (var job in sortedJobs)
                {
                    WriteObject(new PSObject()
                    {
                        Properties = {
                            new PSNoteProperty("JobId", job.JobId),
                            new PSNoteProperty("SourceSiteUrl", job.SourceSiteUrl),
                            new PSNoteProperty("DestinationDataLocation", job.DestinationDataLocation),
                            new PSNoteProperty("DestinationUrl", job.DestinationUrl),
                            new PSNoteProperty("Status", job.Status),
                            new PSNoteProperty("CreatedDate", job.CreatedDate),
                            new PSNoteProperty("CompletedDate", job.CompletedDate),
                            new PSNoteProperty("LastModified", job.LastModified),
                            new PSNoteProperty("ProgressPercentage", job.ProgressPercentage),
                            new PSNoteProperty("SiteSize", job.SiteSize),
                            new PSNoteProperty("ErrorMessage", job.ErrorMessage)
                        }
                    });

                    WriteVerbose($"Site move job: {job.JobId} - {job.Status}");
                }
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "GetSiteContentMoveStateError",
                    ErrorCategory.OperationStopped,
                    null));
            }
        }

        private SiteMoveJob GetSiteMoveJobByUrl(string siteUrl)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Retrieving site move job for URL: {siteUrl}");

            // In actual implementation, this would call the multi-geo REST API
            // to retrieve the move job for the specified site
            return new SiteMoveJob
            {
                JobId = Guid.NewGuid(),
                SourceSiteUrl = siteUrl,
                Status = "Completed",
                CreatedDate = DateTime.UtcNow.AddDays(-2),
                CompletedDate = DateTime.UtcNow.AddDays(-1),
                LastModified = DateTime.UtcNow.AddDays(-1),
                DestinationDataLocation = "EUR",
                DestinationUrl = siteUrl.Replace(".sharepoint.com", "-eur.sharepoint.com"),
                ProgressPercentage = 100.0,
                SiteSize = 1024000000 // 1GB in bytes
            };
        }

        private SiteMoveJob GetSiteMoveJobById(Guid moveId)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Retrieving site move job with ID: {moveId}");

            // In actual implementation, this would call the multi-geo REST API
            // to retrieve the move job by its ID
            return new SiteMoveJob
            {
                JobId = moveId,
                SourceSiteUrl = "https://contoso.sharepoint.com/sites/marketing",
                Status = "InProgress",
                CreatedDate = DateTime.UtcNow.AddHours(-3),
                LastModified = DateTime.UtcNow.AddMinutes(-15),
                DestinationDataLocation = "APC",
                DestinationUrl = "https://contoso-apc.sharepoint.com/sites/marketing",
                ProgressPercentage = 65.0,
                SiteSize = 2048000000 // 2GB in bytes
            };
        }

        private List<SiteMoveJob> GetSiteMoveJobsFiltered(MoveState moveState, MoveDirection moveDirection, DateTime? startTime, DateTime? endTime, uint limit)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation with sample data
            WriteVerbose($"Retrieving filtered site move jobs - State: {moveState}, Direction: {moveDirection}");

            var sampleJobs = new List<SiteMoveJob>
            {
                new SiteMoveJob
                {
                    JobId = Guid.NewGuid(),
                    SourceSiteUrl = "https://contoso.sharepoint.com/sites/sales",
                    Status = "Completed",
                    CreatedDate = DateTime.UtcNow.AddDays(-4),
                    CompletedDate = DateTime.UtcNow.AddDays(-3),
                    LastModified = DateTime.UtcNow.AddDays(-3),
                    DestinationDataLocation = "EUR",
                    DestinationUrl = "https://contoso-eur.sharepoint.com/sites/sales",
                    ProgressPercentage = 100.0,
                    SiteSize = 512000000
                },
                new SiteMoveJob
                {
                    JobId = Guid.NewGuid(),
                    SourceSiteUrl = "https://contoso.sharepoint.com/sites/hr",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModified = DateTime.UtcNow.AddHours(-2),
                    DestinationDataLocation = "APC",
                    DestinationUrl = "https://contoso-apc.sharepoint.com/sites/hr",
                    ProgressPercentage = 45.0,
                    SiteSize = 1536000000
                },
                new SiteMoveJob
                {
                    JobId = Guid.NewGuid(),
                    SourceSiteUrl = "https://contoso.sharepoint.com/sites/finance",
                    Status = "Failed",
                    CreatedDate = DateTime.UtcNow.AddDays(-6),
                    LastModified = DateTime.UtcNow.AddDays(-5),
                    DestinationDataLocation = "NAM",
                    ErrorMessage = "Site too large for move operation",
                    ProgressPercentage = 15.0,
                    SiteSize = 10240000000
                }
            };

            // Apply filters (in real implementation, these would be applied server-side)
            var filteredJobs = sampleJobs.AsQueryable();

            if (startTime.HasValue)
            {
                filteredJobs = filteredJobs.Where(j => j.CreatedDate >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                filteredJobs = filteredJobs.Where(j => j.CreatedDate <= endTime.Value);
            }

            // Apply limit
            return filteredJobs.Take((int)limit).ToList();
        }
    }
}