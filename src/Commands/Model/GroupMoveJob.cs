using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents a unified group move job in SharePoint Multi-Geo
    /// </summary>
    public class GroupMoveJob : IMoveJob
    {
        public Guid JobId { get; set; }
        public string GroupAlias { get; set; }
        public string GroupDisplayName { get; set; }
        public string SourceDataLocation { get; set; }
        public string DestinationDataLocation { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? PreferredMoveBeginDateInUtc { get; set; }
        public DateTime? PreferredMoveEndDateInUtc { get; set; }
        public string ErrorMessage { get; set; }
        public double? ProgressPercentage { get; set; }
        public bool ValidationOnly { get; set; }
        public string SourceSiteUrl { get; set; }
        public string DestinationSiteUrl { get; set; }
        public long? GroupSize { get; set; }
    }
}