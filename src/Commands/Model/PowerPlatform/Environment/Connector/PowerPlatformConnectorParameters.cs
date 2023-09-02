using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorParameters
    {
        [JsonPropertyName("x-ms-apimTemplateParameter.name")]
        public string xmsapimTemplateParametername { get; set; }

        [JsonPropertyName("x-ms-apimTemplateParameter.value")]
        public string xmsapimTemplateParametervalue { get; set; }

        [JsonPropertyName("x-ms-apimTemplateParameter.existsAction")]
        public string xmsapimTemplateParameterexistsAction { get; set; }

        [JsonPropertyName("x-ms-apimTemplate-policySection")]
        public string xmsapimTemplatepolicySection { get; set; }
    }
}