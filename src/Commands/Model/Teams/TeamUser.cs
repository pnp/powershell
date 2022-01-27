using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Teams
{
    public class TeamUser
    {
        [JsonPropertyName("@odata.type")]
        public string Type { get; set; } = "#microsoft.graph.aadUserConversationMember";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; } = new List<string>();

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("tenantId")]
        public string tenantId { get; set; }
        
        [JsonPropertyName("visibleHistoryStartDateTime")]
        public string visibleHistoryStartDateTime { get; set; }
    }
}