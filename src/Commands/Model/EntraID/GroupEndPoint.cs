using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    /// <summary>
    /// Definition of a Microsoft 365 Group Endpoint such as Viva Engage or Teams
    /// </summary>
    public class GroupEndPoint
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("deletedDateTime")]
        public DateTime? DeletedDateTime { get; set; }

        [JsonPropertyName("capability")]
        public string Capability { get; set; }

        [JsonPropertyName("providerId")]
        public string ProviderId { get; set; }

        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("providerResourceId")]
        public string ProviderResourceId { get; set; }
    }
}