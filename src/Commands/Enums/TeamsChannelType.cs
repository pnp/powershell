namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the types of channels that can be created in a Microsoft Teams team
    /// </summary>
    public enum TeamsChannelType
    {
        /// <summary>
        /// Standard channel accessible by all team members
        /// </summary>
        Standard,

        /// <summary>
        /// Private channel only accessible by those explicitly given permissions to the channel
        /// </summary>
        Private,

        /// <summary>
        /// Shared channel allowing for collaboration between multiple tenants
        /// </summary>
        Shared
    }
}
