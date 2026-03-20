using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents a user move job in SharePoint Multi-Geo
    /// </summary>
    public class UserMoveJob
    {
        public Guid JobId { get; set; }
        public string UserPrincipalName { get; set; }
        public string DestinationDataLocation { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? PreferredMoveBeginDateInUtc { get; set; }
        public DateTime? PreferredMoveEndDateInUtc { get; set; }
        public string Reserved { get; set; }
        public string Notify { get; set; }
        public bool ValidationOnly { get; set; }
        public string ErrorMessage { get; set; }
        public double? ProgressPercentage { get; set; }
    }
}