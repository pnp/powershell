using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    public class PermissionScope
    {
        [JsonIgnore]
        public string resourceAppId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "Role";
        
        [JsonIgnore]
        public string Identifier { get; set; }

        /// <summary>
        /// Whether the resource still has this permission enabled. A disabled permission can be written to an app registration, but can
        /// never be granted to it, so disabled permissions are not offered. Must stay out of the payload sent to Microsoft Graph.
        /// </summary>
        [JsonIgnore]
        public bool IsEnabled { get; set; } = true;
    }
}