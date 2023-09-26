using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorBackendService
    {
        [JsonPropertyName("serviceUrl")]
        public string serviceUrl { get; set; }
    }


}
