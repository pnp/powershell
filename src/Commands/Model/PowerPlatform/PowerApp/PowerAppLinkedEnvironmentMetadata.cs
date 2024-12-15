using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppLinkedEnvironmentMetadata
    {
        [JsonPropertyName("resourceId")]
        public Guid ResourceId { get; set; }

        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonPropertyName("uniqueName")]
        public string UniqueName { get; set; }

        [JsonPropertyName("domainName")]
        public string DomainName { get; set; }

        [JsonPropertyName("version")]
        public Version Version { get; set; }

        [JsonPropertyName("instanceUrl")]
        public Uri InstanceUrl { get; set; }

        [JsonPropertyName("instanceApiUrl")]
        public Uri InstanceApiUrl { get; set; }

        [JsonPropertyName("baseLanguage")]
        public long BaseLanguage { get; set; }

        [JsonPropertyName("instanceState")]
        public string InstanceState { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [JsonPropertyName("platformSku")]
        public string PlatformSku { get; set; }
    }
}
