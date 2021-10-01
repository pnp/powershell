using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains metadata on an action or trigger in a Microsoft Power Automate Flow
    /// </summary>
    public class FlowDefinitionTriggerActionMetadata
    {
        /// <summary>
        /// Metadata on the trigger or action used in a Flow
        /// </summary>
        [JsonPropertyName("metadata")]
        public FlowDefinitionTriggerActionMetadataFlowSystem Metadata { get; set; }   
    }
}