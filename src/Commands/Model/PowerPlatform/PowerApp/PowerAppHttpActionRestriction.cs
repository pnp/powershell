using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppHttpActionRestriction
    {
        [JsonPropertyName("appUsesSharepointHttpAction")]
        public bool? AppUsesSharepointHttpAction { get; set; }

        [JsonPropertyName("enforcementStrategy")]
        public string EnforcementStrategy { get; set; }

        [JsonPropertyName("evaluationTime")]
        public DateTimeOffset EvaluationTime { get; set; }
    }
}
