using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents data for creating a site move job
    /// </summary>
    public class SiteMoveJobEntityData
    {
        public string ApiVersion { get; set; }
        public string SourceSiteUrl { get; set; }
        public string DestinationDataLocation { get; set; }
        public string TargetSiteUrl { get; set; }
        public DateTime? PreferredMoveBeginDateInUtc { get; set; }
        public DateTime? PreferredMoveEndDateInUtc { get; set; }
        public string Reserve { get; set; }
        public Enums.MoveOption Option { get; set; }
    }
}