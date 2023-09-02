using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorMetadata
    {
        [JsonPropertyName("sourceType")]
        public string sourceType { get; set; }

        [JsonPropertyName("source")]
        public string source { get; set; }

        [JsonPropertyName("brandColor")]
        public string brandColor { get; set; }

        //[JsonPropertyName("contact")]
        //public PowerPlatformConnectorContact contact { get; set; }

        //[JsonPropertyName("license")]
        //public PowerPlatformConnectorLicense license { get; set; }

        [JsonPropertyName("publisherUrl")]
        public object publisherUrl { get; set; }

        [JsonPropertyName("serviceUrl")]
        public object serviceUrl { get; set; }

        [JsonPropertyName("documentationUrl")]
        public object documentationUrl { get; set; }

        [JsonPropertyName("environmentName")]
        public string environmentName { get; set; }

        [JsonPropertyName("xrmConnectorId")]
        public object xrmConnectorId { get; set; }

        [JsonPropertyName("almMode")]
        public string almMode { get; set; }

        [JsonPropertyName("useNewApimVersion")]
        public bool useNewApimVersion { get; set; }

        [JsonPropertyName("createdBy")]
        public string createdBy { get; set; }

        [JsonPropertyName("modifiedBy")]
        public string modifiedBy { get; set; }

        [JsonPropertyName("allowSharing")]
        public bool allowSharing { get; set; }

        [JsonPropertyName("parameters")]
        public PowerPlatformConnectorParameters parameters { get; set; }
    }
}
