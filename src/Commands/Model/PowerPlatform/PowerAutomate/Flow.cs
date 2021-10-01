using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information on one Microsoft Power Automate Flow
    /// </summary>
    public class Flow
    {
        /// <summary>
        /// Name of the Flow as its Flow GUID
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// Unique identifier of this Flow. Use <see cref="Properties.DisplayName" /> instead to see the friendly name of the Flow as shown through flow.microsoft.com.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of object, typically Microsoft.ProcessSimple/environments/flows
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Additional information on the Flow
        /// </summary>
        [JsonPropertyName("properties")]
        public FlowProperties Properties {get;set;}
    }
}