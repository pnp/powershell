using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Model.Planner
{
    public class Task
    {
        public string PlanId { get; set; }
        public string BucketId { get; set; }
        public string Title { get; set; }
        public string OrderHint { get; set; }
        public string AssigneePriority { get; set; }
        public int PercentComplete { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? DueDateTime { get; set; }
        public bool HasDescription { get; set; }
        public string PreviewType { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public string CompletedBy { get; set; }
        public int ReferenceCount { get; set; }
        public int CheckListItemCount { get; set; }
        public int ActiveChecklistItemCount { get; set; }
        public string ConversationThreadId { get; set; }
        public string Id { get; set; }
        public IdentitySet CreatedBy { get; set; }
        public AppliedCategories AppliedCategories { get; set; }
        public Dictionary<string, TaskAssignment> Assignments { get; set; }
    }
    public class TaskAssignment
    {
        public DateTime AssignedDateTime { get; set; }
        public string OrderHint { get; set; }
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

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalData { get; set; }
    }

}
