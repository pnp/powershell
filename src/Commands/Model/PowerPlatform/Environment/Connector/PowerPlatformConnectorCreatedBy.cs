using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorCreatedBy
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("displayName")]
        public string displayName { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("tenantId")]
        public string tenantId { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public string userPrincipalName { get; set; }
    }
}
