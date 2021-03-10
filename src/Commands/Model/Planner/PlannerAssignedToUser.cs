using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class PlannerAssignedToUser
    {
        [JsonPropertyName("@odata.type")]
        public string Type { get; set; } = "#microsoft.graph.plannerAssignment";

        [JsonPropertyName("orderHint")]
        public string OrderHint { get; set; } = " !";
    }
}