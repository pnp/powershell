using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerAutomate
{
    public class PackageResourceWrapper
    {
        public string Status { get; set; }
        public string[] BaseResourceIds { get; set; }

        public Dictionary<string, PackageResource> Resources { get; set; }
    }
    public class PackageResource
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("creationType")]
        public string CreationType { get; set; }
        [JsonPropertyName("details")]
        public Dictionary<string, string> Details { get; set; }
        [JsonPropertyName("configurableBy")]
        public string ConfigurableBy { get; set; }
        [JsonPropertyName("hierarchy")]
        public string Hierarchy { get; set; }
        [JsonPropertyName("dependsOn")]
        public object[] DependsOn { get; set; }
        [JsonPropertyName("suggestedCreationType")]
        public string SuggestedCreationType { get; set; }
    }
}