using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorCustomParameters
    {
        [JsonPropertyName("loginUri")]
        public PowerPlatformConnectorLoginUri loginUri { get; set; }

        [JsonPropertyName("tenantId")]
        public PowerPlatformConnectorTenantId tenantId { get; set; }

        [JsonPropertyName("resourceUri")]
        public PowerPlatformConnectorResourceUri resourceUri { get; set; }

        [JsonPropertyName("authorizationUrl")]
        public PowerPlatformConnectorAuthorizationUrl authorizationUrl { get; set; }

        [JsonPropertyName("tokenUrl")]
        public PowerPlatformConnectorTokenUrl tokenUrl { get; set; }

        [JsonPropertyName("refreshUrl")]
        public PowerPlatformConnectorRefreshUrl refreshUrl { get; set; }

        [JsonPropertyName("enableOnbehalfOfLogin")]
        public PowerPlatformConnectorEnableOnbehalfOfLogin enableOnbehalfOfLogin { get; set; }
    }
}
