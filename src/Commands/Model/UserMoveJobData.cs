using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents data for creating a user move job
    /// </summary>
    public class UserMoveJobData
    {
        public string UserPrincipalName { get; set; }
        public string DestinationDataLocation { get; set; }
        public DateTime? PreferredMoveBeginDateInUtc { get; set; }
        public DateTime? PreferredMoveEndDateInUtc { get; set; }
        public string Reserved { get; set; }
        public string Notify { get; set; }
        public bool ValidationOnly { get; set; }
    }
}