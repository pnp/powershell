using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    /// <summary>
    /// PasswordCredentials section within an Entra ID Service Principal entity
    /// </summary>
    public class ServicePrincipalPasswordCredentials
    {
        [JsonPropertyName("customKeyIdentifier")]
        public string customKeyIdentifier { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("endDateTime")]
        public DateTime? EndDateTime { get; set; }

        [JsonPropertyName("hint")]
        public string Hint { get; set; }

        [JsonPropertyName("keyId")]
        public Guid? KeyId { get; set; }

        [JsonPropertyName("secretText")]
        public string SecretText { get; set; }

        [JsonPropertyName("startDateTime")]
        public DateTime? StartDateTime { get; set; }                
    }
}
