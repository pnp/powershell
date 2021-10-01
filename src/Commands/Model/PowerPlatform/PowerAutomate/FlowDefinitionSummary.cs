using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information on what action(s) and trigger(s) are used in a Microsoft Power Automate Flow
    /// </summary>
    public class FlowDefinitionSummary
    {
        /// <summary>
        /// Information on trigger(s) used in the Flow
        /// </summary>
        [JsonPropertyName("triggers")]
        public List<FlowDefinitionTriggerAction> Triggers { get; set; }

        /// <summary>
        /// Information on action(s) used in the Flow
        /// </summary>
        [JsonPropertyName("actions")]
        public List<FlowDefinitionTriggerAction> Actions { get; set; }
    }
}