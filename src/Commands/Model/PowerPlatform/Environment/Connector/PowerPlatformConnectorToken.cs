using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorToken
    {
        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("oAuthSettings")]
        public PowerPlatformConnectorOAuthSettings oAuthSettings { get; set; }
    }
}