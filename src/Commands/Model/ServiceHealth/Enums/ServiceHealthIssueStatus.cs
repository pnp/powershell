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
        InvestigationSuspended,
        /// <summary>
        /// After a detailed investigation, the service is confirmed to be healthy and operating as designed. No impact to the service was observed or the cause of the incident originated outside of the service. Incidents and advisories with this status appear in the history view until they expire (after the period of time stated in the final post for that event).
        /// </summary>
        FalsePositive,
        /// <summary>
        /// The service is healthy and no issues have been identified.
        /// </summary>
        ServiceOperational,
        /// <summary>
        /// A potential issue was identified and more information is being gathered about what's going on and the scope of impact.
        /// </summary>
        Investigating,
        /// <summary>
        /// The action has been taken to mitigate the issue and we're verify that the service is healthy.
        /// </summary>
        VerifyingService,
        /// <summary>
        /// You'll see this status if an issue is determined to affect the ability for users to access the service. In this case, the issue is significant and can be reproduced consistently.
        /// </summary>
        ServiceInterruption,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Resolved,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        MitigatedExternal,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Mitigated,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        ResolvedExternal,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Confirmed,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Reported
    }
}