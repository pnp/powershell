using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppTags
    {
        [JsonPropertyName("primaryDeviceWidth")]
        public string PrimaryDeviceWidth { get; set; }

        [JsonPropertyName("primaryDeviceHeight")]
        public string PrimaryDeviceHeight { get; set; }

        [JsonPropertyName("sienaVersion")]
        public string SienaVersion { get; set; }

        [JsonPropertyName("deviceCapabilities")]
        public string DeviceCapabilities { get; set; }

        [JsonPropertyName("supportsPortrait")]
        public string SupportsPortrait { get; set; }

        [JsonPropertyName("supportsLandscape")]
        public string SupportsLandscape { get; set; }

        [JsonPropertyName("primaryFormFactor")]
        public string PrimaryFormFactor { get; set; }

        [JsonPropertyName("publisherVersion")]
        public string PublisherVersion { get; set; }

        [JsonPropertyName("minimumRequiredApiVersion")]
        public string MinimumRequiredApiVersion { get; set; }

        [JsonPropertyName("hasComponent")]
        public string HasComponent { get; set; }

        [JsonPropertyName("hasUnlockedComponent")]
        public string HasUnlockedComponent { get; set; }

        [JsonPropertyName("isUnifiedRootApp")]
        public string IsUnifiedRootApp { get; set; }

        [JsonPropertyName("sp-site-id")]
        public Uri SpSiteId { get; set; }

        [JsonPropertyName("sp-list-id")]
        public Guid? SpListId { get; set; }
    }
}

