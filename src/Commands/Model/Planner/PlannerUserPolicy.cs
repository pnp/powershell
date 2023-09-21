using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    /// <summary>
    /// Contains a Planner User Policy
    /// </summary>
    public class PlannerUserPolicy
    {
        /// <summary>
        /// User Entra ID Identifier or Principal Name to which the policy applies
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Indicates if the user specified in this policy is blocked from deleting tasks that were not created by the user itself
        /// </summary>
        [JsonPropertyName("blockDeleteTasksNotCreatedBySelf")]
        public bool? BlockDeleteTasksNotCreatedBySelf { get; set; }
    }
}