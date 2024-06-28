using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorApiDefinitions
    {
        [JsonPropertyName("originalSwaggerUrl")]
        public string originalSwaggerUrl { get; set; }

        [JsonPropertyName("modifiedSwaggerUrl")]
        public string modifiedSwaggerUrl { get; set; }
    }

}
