using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    /// <summary>
    /// Contains details on a specific resource in a Flow export request
    /// </summary>
    public class PowerAppExportPackageResource
    {
        /// <summary>
        /// Full identifier path of the resource
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Identifier of the resource
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Type of resource
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Indicator if the resource should be updated or considered a new resource by default on import
        /// </summary>
        [JsonPropertyName("creationType")]
        public string CreationType { get; set; }

        /// <summary>
        /// Additional details on the resource
        /// </summary>
        [JsonPropertyName("details")]
        public Dictionary<string, string> Details { get; set; }

        /// <summary>
        /// Indicator who can configure the resource
        /// </summary>
        [JsonPropertyName("configurableBy")]
        public string ConfigurableBy { get; set; }

        /// <summary>
        /// Indicator where this resource is located in a hierarchical structure
        /// </summary>
        [JsonPropertyName("hierarchy")]
        public string Hierarchy { get; set; }

        /// <summary>
        /// Indicator if there are dependencies on other resources
        /// </summary>
        [JsonPropertyName("dependsOn")]
        public object[] DependsOn { get; set; }

        /// <summary>
        /// Suggested approach on import
        /// </summary>
        [JsonPropertyName("suggestedCreationType")]
        public string SuggestedCreationType { get; set; }
    }
}