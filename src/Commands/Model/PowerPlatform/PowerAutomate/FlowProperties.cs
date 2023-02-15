using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains additional information on one Microsoft Power Automate Flow
    /// </summary>
    public class FlowProperties
    {
        /// <summary>
        /// Identifier on the API being used behind the scenes for this Flow
        /// </summary>
        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        /// <summary>
        /// The friendly name of the Flow as can be seen through flow.microsoft.com
        /// </summary>

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Raw state indicating if the Flow is currently enabled or disabled
        /// </summary>
        [JsonPropertyName("state")]
        public string StateRaw { get; set; }

        /// <summary>
        /// State indicating if the Flow is currently enabled or disabled as an enum
        /// </summary>     
        [JsonIgnore]
        public Enums.FlowState? State
        {
            get { return !string.IsNullOrWhiteSpace(StateRaw) && Enum.TryParse<Enums.FlowState>(StateRaw, true, out var result) ? result : (Enums.FlowState?)null; }
            set { StateRaw = value.ToString(); }
        }

        /// <summary>
        /// Raw information on how the Flow has been shared. Returns NULL if the Flow is not shared.
        /// </summary>        
        [JsonPropertyName("sharingType")]
        public string SharingTypeRaw { get; set; }

        /// <summary>
        /// Information on how the Flow has been shared as an enum. Returns NULL if the Flow is not shared.
        /// </summary>     
        [JsonIgnore]
        public Enums.FlowSharingType? SharingType
        {
            get { return !string.IsNullOrWhiteSpace(SharingTypeRaw) && Enum.TryParse<Enums.FlowSharingType>(SharingTypeRaw, true, out var result) ? result : (Enums.FlowSharingType?)null; }
            set { SharingTypeRaw = value.ToString(); }
        }

        /// <summary>
        /// Date and time at which this Flow has been created
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Date and time at which this Flow has last been modified
        /// </summary>
        [JsonPropertyName("lastModifiedTime")]
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// If the flow is in suspended state indicated by <see cref="State"/>, this could give additional information as to why. If not in suspended state, it will return None.
        /// </summary>
        [JsonPropertyName("flowSuspensionReason")]
        public string FlowSuspensionReason { get; set; }

        /// <summary>
        /// A summary on the actions and triggers used in the Flow
        /// </summary>
        [JsonPropertyName("definitionSummary")]
        public FlowDefinitionSummary DefinitionSummary { get; set; }

        /// <summary>
        /// Information on who created the Flow
        /// </summary>
        [JsonPropertyName("creator")]
        public FlowCreator Creator { get; set; }

        /// <summary>
        /// Information on what has provisioned this Flow
        /// </summary>
        [JsonPropertyName("provisioningMethod")]
        public string ProvisioningMethod { get; set; }

        /// <summary>
        /// Boolean indicating if an e-mail alert will be sent to the owner(s) when this Flow fails
        /// </summary>
        [JsonPropertyName("flowFailureAlertSubscribed")]
        public bool? FlowFailureAlertSubscribed { get; set; }

        /// <summary>
        /// Unkwown what this stands for
        /// </summary>
        [JsonPropertyName("workflowEntityId")]
        public string WorkflowEntityId { get; set; }

        /// <summary>
        /// The environment the Flow runs in
        /// </summary>
        [JsonPropertyName("environment")]
        public Environment.Environment EnvironmentDetails { get; set; }

        /// <summary>
        /// Unique identifier of the template used to build this Flow
        /// </summary>
        [JsonPropertyName("templateName")]
        public string TemplateId { get; set; }
    }
}