using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class PlannerPlanDetails
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }

        public Dictionary<string,string> CategoryDescriptions { get; set; }
    }
}