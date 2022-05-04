using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// Represents ViewPoint inside a service update message
    /// </summary>
    public class ServiceUpdateMessageViewPoint
    {
        /// <summary>
        /// Indicates if the message center announcement has been read
        /// </summary>
        [JsonPropertyName("isRead")]
        public bool? IsRead { get; set; }

        /// <summary>
        /// Indicates if the message center announcement has been favored
        /// </summary>
        [JsonPropertyName("isFavorited")]
        public bool? IsFavorited { get; set; }

        /// <summary>
        /// Indicates if the message center announcement has been archived
        /// </summary>
        [JsonPropertyName("isArchived")]
        public bool? IsArchived { get; set; }
    }
}