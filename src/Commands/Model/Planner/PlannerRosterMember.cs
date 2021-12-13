using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    /// <summary>
    /// Contains a Microsoft Planner Roster
    /// </summary>
    public class PlannerRosterMember
    {
        [JsonPropertyName("@odata.type")]
        public string Type { get; set; }
 
        /// <summary>
        /// Unique identifier of the mmber record
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Unique identifier of the user
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Unique identifier of tenant the member is from
        /// </summary>
        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; }        
    }
}