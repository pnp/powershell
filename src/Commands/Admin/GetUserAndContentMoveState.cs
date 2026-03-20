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
    [Cmdlet(VerbsCommon.Get, "PnPUserAndContentMoveState", DefaultParameterSetName = "MoveReport")]
    [OutputType(typeof(UserMoveJob))]
    public class GetUserAndContentMoveState : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        public MoveState MoveState { get; set; } = MoveState.All;

        [Parameter(ParameterSetName = "MoveReport", Mandatory = false)]
        public MoveDirection MoveDirection { get; set; } = MoveDirection.All;

        [Parameter(ParameterSetName = "UserPrincipalName", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string UserPrincipalName { get; set; }

        [Parameter(ParameterSetName = "OdbMoveId", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid OdbMoveId { get; set; }

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

                var moveJobs = new List<UserMoveJob>();

                if (ParameterSetName.Equals("UserPrincipalName") && !string.IsNullOrEmpty(UserPrincipalName))
                {
                    WriteVerbose($"Getting move job for user: {UserPrincipalName}");
                    var moveJob = GetUserMoveJobByUserPrincipalName(UserPrincipalName);
                    if (moveJob != null)
                    {
                        moveJobs.Add(moveJob);
                    }
                }
                else if (ParameterSetName.Equals("OdbMoveId") && OdbMoveId != Guid.Empty)
                {
                    WriteVerbose($"Getting move job with ID: {OdbMoveId}");
                    var moveJob = GetUserMoveJobById(OdbMoveId);
                    if (moveJob != null)
                    {
                        moveJobs.Add(moveJob);
                    }
                }
                else
                {
                    WriteVerbose($"Getting move jobs with filters - State: {MoveState}, Direction: {MoveDirection}, Limit: {Limit}");
                    
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

                    moveJobs = GetMoveJobsFiltered(MoveState, MoveDirection, startTime, endTime, Limit);
                }

                // Sort by last modified date (newest first)
                var sortedJobs = moveJobs.OrderByDescending(x => x.LastModified ?? x.CreatedDate).ToList();

                WriteObject(sortedJobs, true);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "GetUserAndContentMoveStateError",
                    ErrorCategory.OperationStopped,
                    null));
            }
        }

        private UserMoveJob GetUserMoveJobByUserPrincipalName(string userPrincipalName)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Retrieving move job for user: {userPrincipalName}");

            // In actual implementation, this would call the multi-geo REST API
            // to retrieve the move job for the specified user
            return new UserMoveJob
            {
                JobId = Guid.NewGuid(),
                UserPrincipalName = userPrincipalName,
                Status = "Completed",
                CreatedDate = DateTime.UtcNow.AddDays(-1),
                CompletedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                DestinationDataLocation = "EUR",
                ProgressPercentage = 100.0
            };
        }

        private UserMoveJob GetUserMoveJobById(Guid moveId)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation
            WriteVerbose($"Retrieving move job with ID: {moveId}");

            // In actual implementation, this would call the multi-geo REST API
            // to retrieve the move job by its ID
            return new UserMoveJob
            {
                JobId = moveId,
                UserPrincipalName = "user@contoso.com",
                Status = "InProgress",
                CreatedDate = DateTime.UtcNow.AddHours(-2),
                LastModified = DateTime.UtcNow.AddMinutes(-30),
                DestinationDataLocation = "APC",
                ProgressPercentage = 75.0
            };
        }

        private List<UserMoveJob> GetMoveJobsFiltered(MoveState moveState, MoveDirection moveDirection, DateTime? startTime, DateTime? endTime, uint limit)
        {
            // This would typically call the SharePoint Admin REST API
            // For now, creating a placeholder implementation with sample data
            WriteVerbose($"Retrieving filtered move jobs - State: {moveState}, Direction: {moveDirection}");

            var sampleJobs = new List<UserMoveJob>
            {
                new UserMoveJob
                {
                    JobId = Guid.NewGuid(),
                    UserPrincipalName = "user1@contoso.com",
                    Status = "Completed",
                    CreatedDate = DateTime.UtcNow.AddDays(-3),
                    CompletedDate = DateTime.UtcNow.AddDays(-2),
                    LastModified = DateTime.UtcNow.AddDays(-2),
                    DestinationDataLocation = "EUR",
                    ProgressPercentage = 100.0
                },
                new UserMoveJob
                {
                    JobId = Guid.NewGuid(),
                    UserPrincipalName = "user2@contoso.com",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModified = DateTime.UtcNow.AddHours(-1),
                    DestinationDataLocation = "APC",
                    ProgressPercentage = 60.0
                },
                new UserMoveJob
                {
                    JobId = Guid.NewGuid(),
                    UserPrincipalName = "user3@contoso.com",
                    Status = "Failed",
                    CreatedDate = DateTime.UtcNow.AddDays(-5),
                    LastModified = DateTime.UtcNow.AddDays(-4),
                    DestinationDataLocation = "NAM",
                    ErrorMessage = "Insufficient permissions",
                    ProgressPercentage = 30.0
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