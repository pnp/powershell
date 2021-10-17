using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    /// <summary>
    /// Contains a Microsoft Planner Roster
    /// </summary>
    public class PlannerRoster
    {
        /// <summary>
        /// Type of entity
        /// </summary>
        [JsonPropertyName("@odata.type")]
        public string Type { get; set; }
 
        /// <summary>
        /// Unique identifier of the Planner Roster
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Title of the Planner Roster
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Container of the Planner Roster
        /// </summary>
        [JsonPropertyName("container")]
        public PlannerRosterContainer Container { get; set; }         
    }
}