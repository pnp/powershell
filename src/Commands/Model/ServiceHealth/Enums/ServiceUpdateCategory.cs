namespace PnP.PowerShell.Commands.Model.ServiceHealth.Enums
{
    /// <summary>
    /// The category which is assigned to a service update
    /// </summary>
    public enum ServiceUpdateCategory
    {
        /// <summary>
        /// Planning for a change in how things work today
        /// </summary>
        PlanForChange,

        /// <summary>
        /// Staying informed about something that is going to change
        /// </summary>
        StayInformed,

        /// <summary>
        /// Action could be required to prevent or fix an issue at hand
        /// </summary>
        PreventOrFixIssue
    }
}