using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorResourceUri
    {
        [JsonPropertyName("value")]
        public string value { get; set; }
    }
}