using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// Represents a message to mark service update messages as read
    /// </summary>
    public class ServiceUpdateMessageReadStatusBody
    {
        /// <summary>
        /// The list with message Ids that need to be marked as read
        /// </summary>
        [JsonPropertyName("messageIds")]
        public string[] MessageIds { get; set; }
    }
}