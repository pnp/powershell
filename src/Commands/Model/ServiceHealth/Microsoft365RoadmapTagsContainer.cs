using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// A container with tags on an item in the Microsoft 365 Roadmap
    /// </summary>
    public class Microsoft365RoadmapTagsContainer
    {
        /// <summary>
        /// All tagged products to which the roadmap item applies
        /// </summary>
        [JsonPropertyName("products")]
        public List<Microsoft365RoadmapTag> Products { get; set; }

        /// <summary>
        /// All tagged cloud instances for which the roadmap item applies
        /// </summary>
        [JsonPropertyName("cloudInstances")]
        public List<Microsoft365RoadmapTag> CloudInstances { get; set; }

        /// <summary>
        /// All tagged release phases that apply to the roadmap item
        /// </summary>
        [JsonPropertyName("releasePhase")]
        public List<Microsoft365RoadmapTag> ReleasePhase { get; set; }

        /// <summary>
        /// All platforms for which the roadmap item applies
        /// </summary>
        [JsonPropertyName("platforms")]
        public List<Microsoft365RoadmapTag> Platforms { get; set; }
    }
}