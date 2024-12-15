using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class ExecutionRestrictions
    {
        [JsonPropertyName("isTeamsOnly")]
        public bool? IsTeamsOnly { get; set; }

        [JsonPropertyName("dataLossPreventionEvaluationResult")]
        public PowerAppDataLossPreventionEvaluationResult DataLossPreventionEvaluationResult { get; set; }

        [JsonPropertyName("httpActionRestriction")]
        public PowerAppHttpActionRestriction HttpActionRestriction { get; set; }
    }

}
