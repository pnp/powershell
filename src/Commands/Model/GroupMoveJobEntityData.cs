using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents data for creating a group move job
    /// </summary>
    public class GroupMoveJobEntityData
    {
        public string ApiVersion { get; set; }
        public string GroupName { get; set; }
        public string DestinationDataLocation { get; set; }
        public DateTime? PreferredMoveBeginDateInUtc { get; set; }
        public DateTime? PreferredMoveEndDateInUtc { get; set; }
        public string Reserve { get; set; }
        public Enums.MoveOption Option { get; set; }
    }
}