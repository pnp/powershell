﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
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
        public AzureADAuditInitiatedBy InitiatedBy { get; set; }
        [JsonPropertyName("targetResources")]
        public List<AzureADAuditTargetResource> TargetResources { get; set; }
        [JsonPropertyName("additionalDetails")]
        public List<AzureADAuditAdditionalDetail> AdditionalDetails { get; set; }
    }
}
