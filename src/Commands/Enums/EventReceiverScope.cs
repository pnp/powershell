namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Scopes to which an EventReceiver can be targeted
    /// </summary>
    public enum EventReceiverScope
    {
        /// <summary>
        /// Sites
        /// </summary>
        Web,

        /// <summary>
        /// Site collections
        /// </summary>
        Site,

        /// <summary>
        /// Sites collections and sites
        /// </summary>
        All
    }
}
