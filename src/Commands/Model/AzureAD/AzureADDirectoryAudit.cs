using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{

    public class DirectoryAuditAdditionalDetail
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class DirectoryAuditInitiatedBy
    {
        [JsonPropertyName("user")]
        public DirectoryAuditUser User { get; set; }
        [JsonPropertyName("app")]
        public object app { get; set; }
    }

    public class DirectoryAuditModifiedProperty
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("oldValue")]
        public object OldValue { get; set; }
        [JsonPropertyName("newValue")]
        public string NewValue { get; set; }
    }

    public class DirectoryAuditTargetResource
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("modifiedProperties")]
        public List<DirectoryAuditModifiedProperty> ModifiedProperties { get; set; }
        [JsonPropertyName("groupType")]
        public string GroupType { get; set; }
        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }

    public class DirectoryAuditUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
        [JsonPropertyName("ipAddress")]
        public string IPAddress { get; set; }
    }

    public class AzureADDirectoryAudit
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("CorrelationId")]
        public string CorrelationId { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonPropertyName("resultReason")]
        public string ResultReason { get; set; }
        [JsonPropertyName("activityDisplayName")]
        public string ActivityDisplayName { get; set; }
        [JsonPropertyName("activityDateTime")]
        public DateTime ActivityDateTime { get; set; }
        [JsonPropertyName("loggedByService")]
        public string LoggedByService { get; set; }
        [JsonPropertyName("initiatedBy")]
        public DirectoryAuditInitiatedBy InitiatedBy { get; set; }
        [JsonPropertyName("targetResources")]
        public List<DirectoryAuditTargetResource> TargetResources { get; set; }
        [JsonPropertyName("additionalDetails")]
        public List<DirectoryAuditAdditionalDetail> AdditionalDetails { get; set; }
    }
}
