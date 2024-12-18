using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADAuditUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
        [JsonPropertyName("ipAddress")]
        public string IPAddress { get; set; }
    }
}
