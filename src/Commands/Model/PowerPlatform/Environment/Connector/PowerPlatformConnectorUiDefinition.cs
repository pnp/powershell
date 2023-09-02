using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorUiDefinition
    {
        [JsonPropertyName("constraints")]
        public PowerPlatformConnectorConstraints constraints { get; set; }
    }
}