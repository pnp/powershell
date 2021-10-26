using System;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class PlannerPlan
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string Owner { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public IdentitySet CreatedBy { get; set; }
    }
}