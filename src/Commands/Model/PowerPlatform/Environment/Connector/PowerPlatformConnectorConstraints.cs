using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorConstraints
    {
        [JsonPropertyName("required")]
        public string required { get; set; }

        [JsonPropertyName("hidden")]
        public string hidden { get; set; }
    }
}
