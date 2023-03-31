using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppDataLossPreventionEvaluationResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("lastEvaluationDate")]
        public DateTimeOffset LastEvaluationDate { get; set; }

        [JsonPropertyName("violationDetails")]
        public object[] ViolationDetails { get; set; }

        [JsonPropertyName("violations")]
        public object[] Violations { get; set; }

        [JsonPropertyName("violationsByPolicy")]
        public object[] ViolationsByPolicy { get; set; }

        [JsonPropertyName("violationErrorMessage")]
        public string ViolationErrorMessage { get; set; }
    }
}
