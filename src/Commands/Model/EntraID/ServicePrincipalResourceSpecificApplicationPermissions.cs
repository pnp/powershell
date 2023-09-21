using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    /// <summary>
    /// ResourceSpecificApplicationPermissions section within an Entra ID Service Principal entity
    /// </summary>
    public class ServicePrincipalResourceSpecificApplicationPermissions
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("isEnabled")]
        public bool? IsEnabled { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }                        
    }
}
