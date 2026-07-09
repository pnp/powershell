using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ToDo
{
    public class LinkedResource
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("applicationName")]
        public string ApplicationName { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; }

        [JsonPropertyName("webUrl")]
        public string WebUrl { get; set; }
    }
}
