using System;
using System.Text.Json.Serialization;
/// <summary>
/// Describes the retention label that details how to Represents how customers can manage their data, including whether and for how long to retain or delete it."
/// </summary>
/// <seealso cref="https://learn.microsoft.com/en-gb/graph/api/resources/security-retentionlabel"/>
namespace PnP.PowerShell.Commands.Model.Graph.Purview
{
    public class RetentionLabel
    {
        /// <summary>
        /// The label ID is a globally unique identifier (GUID).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The display name of the label.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Behavior during the retention period. Specifies how the behavior of a document with this label should be during the retention period. The possible values are: doNotRetain, retain, retainAsRecord, retainAsRegulatoryRecord, unknownFutureValue.
        /// </summary>
        public BehaviorDuringRetentionPeriod? BehaviorDuringRetentionPeriod { get; set; }

        /// <summary>
        /// Action after the retention period.Specifies the action to take on a document with this label applied during the retention period. The possible values are: none, delete, startDispositionReview, unknownFutureValue.
        /// </summary>
        public ActionAfterRetentionPeriod? ActionAfterRetentionPeriod { get; set; }

        /// <summary>
        /// Retention trigger information. Specifies whether the retention duration is calculated from the content creation date, labeled date, or last modification date. The possible values are: dateLabeled, dateCreated, dateModified, dateOfEvent, unknownFutureValue.
        /// </summary>
        public RetentionTrigger?  RetentionTrigger { get; set; }

        /// <summary>
        /// Retention duration information. Specifies the number of days to retain the content.
        /// </summary>
        [JsonPropertyName("retentionDuration")]
        public RetentionDuration RetentionDuration { get; set; }

        /// <summary>
        /// Indicates if the label is in use.
        /// </summary>
        public bool? IsInUse { get; set; }

        /// <summary>
        /// Description for administrators.
        /// </summary>
        public string DescriptionForAdmins { get; set; }

        /// <summary>
        /// Description for users.
        /// </summary>
        public string DescriptionForUsers { get; set; }

        /// <summary>
        /// Information about the creator.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public IdentitySet CreatedBy { get; set; }

        /// <summary>
        /// Date and time when the label was created.
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Information about the last modifier.
        /// </summary>
        [JsonPropertyName("lastModifiedBy")]
        public IdentitySet LastModifiedBy { get; set; }

        /// <summary>
        /// Date and time when the label was last modified.
        /// </summary>
        public DateTimeOffset LastModifiedDateTime { get; set; }

        /// <summary>
        /// The label to be applied. Specifies the replacement label to be applied automatically after the retention period of the current label ends.
        /// </summary>
        public string LabelToBeApplied { get; set; }
            
        /// <summary>
        /// Default record behavior.Specifies the locked or unlocked state of a record label when it is created.The possible values are: startLocked, startUnlocked, unknownFutureValue.
        /// </summary>
        public DefaultRecordBehavior DefaultRecordBehavior { get; set; }
    }

    public enum BehaviorDuringRetentionPeriod
    {
        DoNotRetain,
        Retain,
        RetainAsRecord,
        RetainAsRegulatoryRecord
    }

    public enum ActionAfterRetentionPeriod
    {
        None,
        Delete,
        StartDispositionReview
    }

    public enum RetentionTrigger
    {
        DateLabeled,
        DateCreated,
        DateModified,
        DateOfEvent
    }

    public enum DefaultRecordBehavior
    {
        StartLocked,
        StartUnlocked
    }
}
