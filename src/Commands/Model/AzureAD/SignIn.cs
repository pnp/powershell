using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class SignIn
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonPropertyName("userDisplayName")]
        public string UserDisplayName { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("appId")]
        public string AppId { get; set; }

        [JsonPropertyName("appDisplayName")]
        public string AppDisplayName { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("clientAppUsed")]
        public string ClientAppUsed { get; set; }

        [JsonPropertyName("correlationId")]
        public string CorrelationId { get; set; }

        [JsonPropertyName("conditionalAccessStatus")]
        public string ConditionalAccessStatus { get; set; }

        [JsonPropertyName("isInteractive")]
        public bool IsInteractive { get; set; }

        [JsonPropertyName("riskDetail")]
        public string RiskDetail { get; set; }

        [JsonPropertyName("riskLevelAggregated")]
        public string RiskLevelAggregated { get; set; }

        [JsonPropertyName("riskLevelDuringSignIn")]
        public string RiskLevelDuringSignIn { get; set; }

        [JsonPropertyName("riskState")]
        public string RiskState { get; set; }

        [JsonPropertyName("riskEventTypes")]
        public List<object> RiskEventTypes { get; set; }

        [JsonPropertyName("resourceDisplayName")]
        public string ResourceDisplayName { get; set; }

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }

        [JsonPropertyName("status")]
        public SignInStatus Status { get; set; }

        [JsonPropertyName("deviceDetail")]
        public SignInDeviceDetail DeviceDetail { get; set; }

        [JsonPropertyName("location")]
        public SignInLocation Location { get; set; }

        [JsonPropertyName("appliedConditionalAccessPolicies")]
        public List<SignInAppliedConditionalAccessPolicy> AppliedConditionalAccessPolicies { get; set; }
    }
}
