using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    /// <summary>
    /// Oauth2PermissionScopes section within an Entra ID Service Principal entity
    /// </summary>
    public class ServicePrincipalOauth2PermissionScopes
    {
        [JsonPropertyName("adminConsentDescription")]
        public string AdminConsentDescription { get; set; }

        [JsonPropertyName("adminConsentDisplayName")]
        public string AdminConsentDisplayName { get; set; }

        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("isEnabled")]
        public bool? IsEnabled { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("userConsentDisplayName")]
        public string UserConsentDisplayName { get; set; }

        [JsonPropertyName("userConsentDescription")]
        public string UserConsentDescription { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }                        
    }
}
