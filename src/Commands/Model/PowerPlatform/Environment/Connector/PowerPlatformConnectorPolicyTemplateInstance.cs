using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorPolicyTemplateInstance
    {
        [JsonPropertyName("templateId")]
        public string templateId { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("parameters")]
        public PowerPlatformConnectorParameters parameters { get; set; }
    }
}