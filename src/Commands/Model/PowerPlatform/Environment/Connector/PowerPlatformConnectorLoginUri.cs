using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorLoginUri
    {
        [JsonPropertyName("value")]
        public string value { get; set; }
    }
}
