namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// All allowed options for Team Channel post new message restrictions within the moderation settings of a Microsoft Teams team
    /// </summary>
    /// <remarks>Documentation: https://learn.microsoft.com/graph/api/resources/channelmoderationsettings#properties</remarks>
    public enum TeamChannelModerationSettingNewMessageRestriction
    {
        /// <summary>
        /// Everyone can create new posts in the channel
        /// </summary>
        Everyone,

        /// <summary>
        /// Everyone except guests can create new posts in the channel
        /// </summary>
        EveryoneExceptGuests,

        /// <summary>
        /// Moderators can create new posts in the channel
        /// </summary>
        Moderators
    }
}
