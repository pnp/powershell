using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Teams
{
    /// <summary>
    /// Moderation settings on a Teams Channel
    /// </summary>
    /// <remarks>Documentation at https://learn.microsoft.com/graph/api/resources/channelmoderationsettings</remarks>
    public partial class ChannelModerationSettings
    {
        #region Public Members

        /// <summary>
        /// Indicates whether bots are allowed to post messages
        /// </summary>
        [JsonPropertyName("allowNewMessageFromBots")]
        public bool? AllowNewMessageFromBots { get; set; }

        /// <summary>
        /// Indicates whether connectors are allowed to post messages
        /// </summary>
        [JsonPropertyName("allowNewMessageFromConnectors")]
        public bool? AllowNewMessageFromConnectors { get; set; }

        /// <summary>
        /// Indicates who is allowed to reply to the teams channel
        /// </summary>
        [JsonPropertyName("replyRestriction")]
        public Enums.TeamChannelModerationSettingReplyRestriction? ReplyRestriction { get; set; }

        /// <summary>
        /// Indicates who is allowed to post messages to teams channel
        /// </summary>
        [JsonPropertyName("userNewMessageRestriction")]
        public Enums.TeamChannelModerationSettingNewMessageRestriction? UserNewMessageRestriction { get; set; }

        #endregion
    }
}
