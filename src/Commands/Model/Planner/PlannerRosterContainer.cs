using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    /// <summary>
    /// Contains a container inside a Microsoft Planner Roster
    /// </summary>
    public class PlannerRosterContainer
    {

        /// <summary>
        /// Unique identifier of the Planner Roster Container
        /// </summary>
        [JsonPropertyName("containerId")]
        public string ContainerId { get; set; }

        /// <summary>
        /// Microsoft Graph URL through which the Roster Container can be queried
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Type of container
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }         
    }
}