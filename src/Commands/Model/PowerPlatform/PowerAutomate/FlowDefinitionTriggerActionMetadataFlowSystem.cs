using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains Flow system metadata on an action or trigger in a Microsoft Power Automate Flow
    /// </summary>
    public class FlowDefinitionTriggerActionMetadataFlowSystem
    {
        /// <summary>
        /// Identifier of the operation used in the action or trigger
        /// </summary>
        [JsonPropertyName("swaggerOperationId")]
        public string SwaggerOperationId { get; set; }      
    }
}