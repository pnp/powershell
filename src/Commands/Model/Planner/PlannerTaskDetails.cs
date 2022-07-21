using PnP.PowerShell.Commands.Model.Graph;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class PlannerTaskDetails
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }

        public string Id { get; set; }

        public string Description { get; set; }

        public string PreviewType { get; set; }

        public Dictionary<string, PlannerTaskExternalReference> References { get; set; }
        public Dictionary<string, PlannerTaskCheckListItem> Checklist { get; set; }
    }

    public class PlannerTaskExternalReference
    {
        public string Alias { get; set; }
        public string Type { get; set; }
        public string PreviewPriority { get; set; }
        public IdentitySet LastModifiedBy { get; set; }
    }

    public class PlannerTaskCheckListItem
    {
        public bool? IsChecked { get; set; }
        public string Title { get; set; }
        public string OrderHint { get; set; }
        public IdentitySet LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
