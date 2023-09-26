using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorTokenTenantId
    {
        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("metadata")]
        public PowerPlatformConnectorMetadata metadata { get; set; }

        [JsonPropertyName("uiDefinition")]
        public PowerPlatformConnectorUiDefinition uiDefinition { get; set; }
    }
}