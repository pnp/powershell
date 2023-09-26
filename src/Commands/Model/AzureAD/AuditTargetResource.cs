using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AuditTargetResource
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("modifiedProperties")]
        public List<AuditModifiedProperty> ModifiedProperties { get; set; }
        [JsonPropertyName("groupType")]
        public string GroupType { get; set; }
        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }
}
