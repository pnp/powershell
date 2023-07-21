namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Scopes to which flow sharing status is defined
    /// </summary>
    public enum FlowSharingStatus
    {
        /// <summary>
        /// returns flows that are shared with you, or created by you and shared with someone
        /// </summary>
        SharedWithMe,

        /// <summary>
        /// returns all flows created by you which are not shared.
        /// </summary>
        Personal,

        /// <summary>
        /// Combines personal and sharedwith me
        /// </summary>
        All

    }
}
