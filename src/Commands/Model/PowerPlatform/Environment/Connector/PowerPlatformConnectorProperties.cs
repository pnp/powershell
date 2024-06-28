using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorProperties
    {
        [JsonPropertyName("displayName")]
        public string displayName { get; set; }

        [JsonPropertyName("iconUri")]
        public string iconUri { get; set; }

        [JsonPropertyName("iconBrandColor")]
        public string iconBrandColor { get; set; }

        //[JsonPropertyName("contact")]
        //public PowerPlatformConnectorContact contact { get; set; }

        //[JsonPropertyName("license")]
        //public PowerPlatformConnectorLicense license { get; set; }

        [JsonPropertyName("apiEnvironment")]
        public string apiEnvironment { get; set; }

        [JsonPropertyName("isCustomApi")]
        public bool isCustomApi { get; set; }

        [JsonPropertyName("connectionParameters")]
        public PowerPlatformConnectorConnectionParameters connectionParameters { get; set; }

        [JsonPropertyName("runtimeUrls")]
        public List<string> runtimeUrls { get; set; }

        [JsonPropertyName("primaryRuntimeUrl")]
        public string primaryRuntimeUrl { get; set; }

        [JsonPropertyName("metadata")]
        public PowerPlatformConnectorMetadata metadata { get; set; }

        [JsonPropertyName("capabilities")]
        public List<object> capabilities { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("apiDefinitions")]
        public PowerPlatformConnectorApiDefinitions apiDefinitions { get; set; }

        [JsonPropertyName("backendService")]
        public PowerPlatformConnectorBackendService backendService { get; set; }

        [JsonPropertyName("createdBy")]
        public PowerPlatformConnectorCreatedBy createdBy { get; set; }

        [JsonPropertyName("modifiedBy")]
        public PowerPlatformConnectorModifiedBy modifiedBy { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTimeOffset createdTime { get; set; }

        [JsonPropertyName("changedTime")]
        public DateTimeOffset changedTime { get; set; }

        [JsonPropertyName("environment")]
        public PowerPlatformConnectorEnvironment environment { get; set; }

        [JsonPropertyName("tier")]
        public string tier { get; set; }

        [JsonPropertyName("publisher")]
        public string publisher { get; set; }

        [JsonPropertyName("almMode")]
        public string almMode { get; set; }

        [JsonPropertyName("parameters")]
        public PowerPlatformConnectorParameters parameters { get; set; }

        [JsonPropertyName("policyTemplateInstances")]
        public List<PowerPlatformConnectorPolicyTemplateInstance> policyTemplateInstances { get; set; }

        [JsonPropertyName("IsFirstParty")]
        public string IsFirstParty { get; set; }

        [JsonPropertyName("AzureActiveDirectoryResourceId")]
        public string AzureActiveDirectoryResourceId { get; set; }

        [JsonPropertyName("IsOnbehalfofLoginSupported")]
        public bool IsOnbehalfofLoginSupported { get; set; }
    }
}