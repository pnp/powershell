namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defiens the states the dynamic membership processor can be in
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/group#properties"/>
    public enum DynamicMembershipRuleProcessingState : short
    {
        /// <summary>
        /// The dynamic membership rule is enabled and will be evaluated
        /// </summary>
        On = 0,

        /// <summary>
        /// The dynamic membership rule is disabled and will not be evaluated
        /// </summary>
        Paused = 1
    }
}

