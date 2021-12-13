namespace PnP.PowerShell.Commands.Model.ServiceHealth.Enums
{
    /// <summary>
    /// The state a service health issue can be in
    /// </summary>
    public enum ServiceHealthIssueStatus
    {
        /// <summary>
        /// The services have been restored to their normal behavior
        /// </summary>
        ServiceRestored,

        /// <summary>
        /// Information has been published on how the incident could have happened
        /// </summary>
        PostIncidentReviewPublished,

        /// <summary>
        /// The service is not completely working as it should
        /// </summary>
        ServiceDegradation,

        /// <summary>
        /// Service is being restored to its normal behavior
        /// </summary>
        RestoringService,

        /// <summary>
        /// Service is still being restored to its normal behavior
        /// </summary>
        ExtendedRecovery,

        /// <summary>
        /// Investigation towards the cause of the issue has been suspended
        /// </summary>
        InvestigationSuspended
    }
}