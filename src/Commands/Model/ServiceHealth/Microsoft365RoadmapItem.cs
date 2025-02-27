using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// An item on the Microsoft 365 Roadmap
    /// </summary>
    public class Microsoft365RoadmapItem
    {
        /// <summary>
        /// The ID of the roadmap item
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Title of the roadmap item
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the roadmap item
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// An optional link to a page holding more information regarding the roadmap item
        /// </summary>
        [JsonPropertyName("moreInfoLink")]
        public string MoreInfoLink { get; set; }

        /// <summary>
        /// Date at which the roadmap item is expected to become generally available
        /// </summary>
        [JsonPropertyName("publicDisclosureAvailabilityDate")]
        public string PublicDisclosureAvailabilityDate { get; set; }

        /// <summary>
        /// Date at which the roadmap item is expectedo to become available in preview
        /// </summary>
        [JsonPropertyName("publicPreviewDate")]
        public string PublicPreviewDate { get; set; }

        /// <summary>
        /// Date and time at which this roadmap item was initially added
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Unknown status
        /// </summary>
        [JsonPropertyName("publicRoadmapStatus")]
        public string PublicRoadmapStatus { get; set; }

        /// <summary>
        /// Rollout status of the roadmap item
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Date and time at which this roadmap item has last been modified
        /// </summary>
        [JsonPropertyName("modified")]
        public DateTime? Modified { get; set; }

        /// <summary>
        /// All tags applicable to this roadmap item
        /// </summary>
        [JsonPropertyName("tags")]
        public List<Microsoft365RoadmapTag> Tags { get; set; }

        /// <summary>
        /// Tags applicable to this roadmap item, categorized by type of tag
        /// </summary>
        [JsonPropertyName("tagsContainer")]
        public Microsoft365RoadmapTagsContainer TagsContainer { get; set; }
    }
}