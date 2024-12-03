using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.Purview
{
    using System;
    using System.Text.Json.Serialization;

    public class FileRetentionLabel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("retentionSettings")]
        public RetentionSettings RetentionSettings { get; set; }

        [JsonPropertyName("isLabelAppliedExplicitly")]
        public bool IsLabelAppliedExplicitly { get; set; }

        [JsonPropertyName("labelAppliedDateTime")]
        public DateTime LabelAppliedDateTime { get; set; }

        [JsonPropertyName("labelAppliedBy")]
        public LabelAppliedBy LabelAppliedBy { get; set; }
    }

    public class RetentionSettings
    {
        [JsonPropertyName("behaviorDuringRetentionPeriod")]
        public string BehaviorDuringRetentionPeriod { get; set; }

        [JsonPropertyName("isDeleteAllowed")]
        public bool IsDeleteAllowed { get; set; }

        [JsonPropertyName("isRecordLocked")]
        public bool IsRecordLocked { get; set; }

        [JsonPropertyName("isMetadataUpdateAllowed")]
        public bool IsMetadataUpdateAllowed { get; set; }

        [JsonPropertyName("isContentUpdateAllowed")]
        public bool IsContentUpdateAllowed { get; set; }

        [JsonPropertyName("isLabelUpdateAllowed")]
        public bool IsLabelUpdateAllowed { get; set; }
    }

    public class LabelAppliedBy
    {
        [JsonPropertyName("user")]
        public User User { get; set; }
    }

    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

}

