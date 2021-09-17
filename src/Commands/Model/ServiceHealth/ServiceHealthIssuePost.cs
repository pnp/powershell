using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// Represents an informal post on a service health issue
    /// </summary>
    public class ServiceHealthIssuePost
    {
        /// <summary>
        /// Date and time at which this post has been created
        /// </summary>
        [JsonPropertyName("createdDateTime")]
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// Type of post
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("postType")]
        public Enums.ServiceHealthIssuePostType? PostType { get; set; }

        /// <summary>
        /// Contains the informational message of this service health issue
        /// </summary>
        [JsonPropertyName("description")]
        public ServiceHealthIssuePostDescription Description { get; set; }
    }
}