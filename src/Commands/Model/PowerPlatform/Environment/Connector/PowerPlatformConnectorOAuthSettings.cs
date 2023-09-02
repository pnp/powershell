using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorOAuthSettings
    {
        [JsonPropertyName("identityProvider")]
        public string identityProvider { get; set; }

        [JsonPropertyName("clientId")]
        public string clientId { get; set; }

        [JsonPropertyName("scopes")]
        public List<string> scopes { get; set; }

        [JsonPropertyName("redirectMode")]
        public string redirectMode { get; set; }

        [JsonPropertyName("redirectUrl")]
        public string redirectUrl { get; set; }

        [JsonPropertyName("properties")]
        public PowerPlatformConnectorProperties properties { get; set; }

        [JsonPropertyName("customParameters")]
        public PowerPlatformConnectorCustomParameters customParameters { get; set; }
    }
}
