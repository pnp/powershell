using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorAuthorizationUrl
    {
        [JsonPropertyName("value")]
        public string value { get; set; }
    }
}
