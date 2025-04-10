using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities.JSON;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class PlannerTask
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }
        public string PlanId { get; set; }
        public string BucketId { get; set; }
        public string Title { get; set; }
        public string OrderHint { get; set; }
        public string AssigneePriority { get; set; }
        public int? PercentComplete { get; set; }
        public int? Priority { get; set; }

        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? StartDateTime { get; set; }

        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? CreatedDateTime { get; set; }

        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? DueDateTime { get; set; }

        public bool? HasDescription { get; set; }
        public string PreviewType { get; set; }

        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? CompletedDateTime { get; set; }

        public IdentitySet CompletedBy { get; set; }
        public int? ReferenceCount { get; set; }
        public int? CheckListItemCount { get; set; }
        public int? ActiveChecklistItemCount { get; set; }
        public string ConversationThreadId { get; set; }
        public string Id { get; set; }
        public IdentitySet CreatedBy { get; set; }
        public AppliedCategories AppliedCategories { get; set; }
        public Dictionary<string, TaskAssignment> Assignments { get; set; }

        public PlannerTaskDetails Details { get; internal set; }
    }
    public class TaskAssignment
    {
        [JsonPropertyName("@odata.type")]
        public string Type { get; set; } = "#microsoft.graph.plannerAssignment";

        public DateTime? AssignedDateTime { get; set; }
        public string OrderHint { get; set; } = " !";
        public IdentitySet AssignedBy { get; set; }
    }

    public class AppliedCategories
    {
        public bool? Category1 { get; set; }
        public bool? Category2 { get; set; }
        public bool? Category3 { get; set; }
        public bool? Category4 { get; set; }
        public bool? Category5 { get; set; }
        public bool? Category6 { get; set; }
        public bool? Category7 { get; set; }
        public bool? Category8 { get; set; }
        public bool? Category9 { get; set; }
        public bool? Category10 { get; set; }
        public bool? Category11 { get; set; }
        public bool? Category12 { get; set; }
        public bool? Category13 { get; set; }
        public bool? Category14 { get; set; }
        public bool? Category15 { get; set; }
        public bool? Category16 { get; set; }
        public bool? Category17 { get; set; }
        public bool? Category18 { get; set; }
        public bool? Category19 { get; set; }
        public bool? Category20 { get; set; }
        public bool? Category21 { get; set; }
        public bool? Category22 { get; set; }
        public bool? Category23 { get; set; }
        public bool? Category24 { get; set; }
        public bool? Category25 { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}
