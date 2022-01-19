using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information on an action or trigger in a Microsoft Power Automate Flow
    /// </summary>
    public class FlowDefinitionTriggerAction
    {
        /// <summary>
        /// Type of action or trigger used in the Flow
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// More information on the kind of action of trigger
        /// </summary>
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        /// <summary>
        /// The specific operation used in this action or trigger
        /// </summary>
        [JsonPropertyName("swaggerOperationId")]
        public string SwaggerOperationId { get; set; }        
    }
}