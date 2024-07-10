namespace PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement
{
    /// <summary>
    /// Defines an expiration of a scheduled role assignment within Privileged Identity Management
    /// </summary>
    public class Expiration
    {
        /// <summary>
        /// The type of expiration, i.e. AfterDuration or AtDateTime
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The duration of the expiration, e.g. PT4H to indicate 4 hours
        /// </summary>
        public string Duration { get; set; }
    }
}
