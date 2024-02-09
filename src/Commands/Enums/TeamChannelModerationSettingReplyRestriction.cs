namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// All allowed options for Team Channel reply restrictions within the moderation settings of a Microsoft Teams team
    /// </summary>
    /// <remarks>Documentation: https://learn.microsoft.com/graph/api/resources/channelmoderationsettings#properties</remarks>
    public enum TeamChannelModerationSettingReplyRestriction
    {
        /// <summary>
        /// Everyone can reply in the channel
        /// </summary>
        Everyone,

        /// <summary>
        /// Only authors and moderators can reply in the channel
        /// </summary>
        AuthorAndModerators
    }
}
