namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate.Enums
{
    /// <summary>
    /// Contains the possible states a Flow can be in
    /// </summary>
    public enum FlowState
    {
        /// <summary>
        /// Flow has been suspended because it has failed too many times in the past
        /// </summary>
        Suspended,

        /// <summary>
        /// Flow has manually been disabled
        /// </summary>
        Stopped,

        /// <summary>
        /// Flow is enabled
        /// </summary>
        Started
    }
}