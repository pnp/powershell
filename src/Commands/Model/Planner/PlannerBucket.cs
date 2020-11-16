using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class PlannerBucket
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }
        public string Name { get; set; }
        public string PlanId { get; set; }
        public string OrderHint { get; set; }
        public string Id { get; set; }
    }
}
