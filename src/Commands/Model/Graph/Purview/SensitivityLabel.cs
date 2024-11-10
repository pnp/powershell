using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.Purview
{
    public class SensitivityLabel
    {
        [JsonPropertyName("sensitivityLabelId")]
        public string SensitivityLabelId { get; set; }

        [JsonPropertyName("assignmentMethod")]
        public string AssignmentMethod { get; set; }

        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; }
    }

    public class SensitivityLabels
    {
        [JsonPropertyName("@odata.type")]
        public string Odatatype { get; set; }

        [JsonPropertyName("labels")]
        public List<SensitivityLabel> Labels { get; set; }
    }
}
