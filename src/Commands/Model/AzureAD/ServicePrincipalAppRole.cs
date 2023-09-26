using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class ServicePrincipalAppRole
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("isEnabled")]
        public bool? IsEnabled { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }  

        [JsonPropertyName("allowedMemberTypes")]
        public string[] AllowedMemberTypes { get; set; } 

        [JsonIgnore]
        public ServicePrincipal ServicePrincipal { get; set; }
    }
}
