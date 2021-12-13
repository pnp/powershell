using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on a Power Platform environment
    /// </summary>
    public class EnvironmentProperties
    {
        /// <summary>
        /// The friendly displayname of a Power Platform environment
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Date and time at which this environment has been created
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Details on who has created this environment
        /// </summary>
        [JsonPropertyName("createdBy")]
        public EnvironmentUser CreatedBy { get; set; }

        /// <summary>
        /// Date and time at which this environment has last been modified
        /// </summary>
        [JsonPropertyName("lastModifiedTime")]
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// Details on who has last modified this environment
        /// </summary>
        [JsonPropertyName("lastModifiedBy")]
        public EnvironmentUser LastModifiedBy { get; set; }

        /// <summary>
        /// Status of the provisioning of this environment
        /// </summary>
        [JsonPropertyName("provisioningState")]
        public string ProvisioningState { get; set; }

        /// <summary>
        /// Raw indicator what created this environment
        /// </summary>
        [JsonPropertyName("creationType")]
        public string CreationTypeRaw { get; set; }

        /// <summary>
        /// Indicator what created the environment as an enum. Returns NULL if unknown.
        /// </summary>     
        [JsonIgnore]
        public Enums.EnvironmentCreationType? CreationType
        {
            get { return !string.IsNullOrWhiteSpace(CreationTypeRaw) && Enum.TryParse<Enums.EnvironmentCreationType>(CreationTypeRaw, true, out var result) ? result : (Enums.EnvironmentCreationType?)null; }
            set { CreationTypeRaw = value.ToString(); }
        }

        /// <summary>
        /// Raw indicator for the type of the type of environment
        /// </summary>
        [JsonPropertyName("environmentSku")]
        public string EnvironmentSkuRaw { get; set; }
        
        /// <summary>
        /// Indicator of the type of environment as an enum. Returns NULL if unknown.
        /// </summary>     
        [JsonIgnore]
        public Enums.EnvironmentSku? EnvironmentSku
        {
            get { return !string.IsNullOrWhiteSpace(EnvironmentSkuRaw) && Enum.TryParse<Enums.EnvironmentSku>(EnvironmentSkuRaw, true, out var result) ? result : (Enums.EnvironmentSku?)null; }
            set { EnvironmentSkuRaw = value.ToString(); }
        }

        /// <summary>
        /// Subtype of the environment
        /// </summary>
        [JsonPropertyName("environmentType")]
        public string EnvironmentType { get; set; }

        /// <summary>
        /// Current state of the environment
        /// </summary>
        [JsonPropertyName("states")]
        public EnvironmentStates States { get; set; }

        /// <summary>
        /// Boolean indicating if this is the default environment
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool? IsDefault { get; set; }

        /// <summary>
        /// Region in which the environment is deployed
        /// </summary>
        [JsonPropertyName("azureRegionHint")]
        public string AzureRegionHint { get; set; }

        /// <summary>
        /// Information on the endpoints through which this environment is available
        /// </summary>
        [JsonPropertyName("runtimeEndpoints")]
        public Dictionary<string, string> RuntimeEndpoints { get; set; }

        /// <summary>
        /// Details on the Dynamics instance linked to this Power Platform environment
        /// </summary>
        [JsonPropertyName("linkedEnvironmentMetadata")]
        public EnvironmentLinkedEnvironmentMetadata LinkedEnvironmentMetadata { get; set; }

        /// <summary>
        /// Specific features on this environment and its enabled state
        /// </summary>
        [JsonPropertyName("environmentFeatures")]
        public EnvironmentFeatures EnvironmentFeatures { get; set; }
    }
}