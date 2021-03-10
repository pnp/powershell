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
    }
}