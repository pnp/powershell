namespace PnP.PowerShell.Commands.Model.ServiceHealth.Enums
{
    /// <summary>
    /// The classification of a service health issue
    /// </summary>
    public enum ServiceHealthIssueClassification
    {
        /// <summary>
        /// Issue is an adivsory on how to act
        /// </summary>
        Advisory,

        /// <summary>
        /// Issue is an incident to inform
        /// </summary>
        Incident
    }
}