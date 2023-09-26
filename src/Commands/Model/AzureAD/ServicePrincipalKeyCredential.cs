using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class ServicePrincipalKeyCredential
    {
        [JsonPropertyName("customKeyIdentifier")]
        public string CustomKeyIdentifier { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonPropertyName("key")]
        public object Key { get; set; }

        [JsonPropertyName("keyId")]
        public string KeyId { get; set; }

        [JsonPropertyName("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("usage")]
        public string Usage { get; set; }
    }
}
