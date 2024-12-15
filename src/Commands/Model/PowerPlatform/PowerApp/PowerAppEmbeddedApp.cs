using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppEmbeddedApp
    {
        [JsonPropertyName("siteId")]
        public Uri SiteId { get; set; }

        [JsonPropertyName("listId")]
        public Guid ListId { get; set; }

        [JsonPropertyName("listUrl")]
        public Uri ListUrl { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("screenWidth")]
        public long ScreenWidth { get; set; }

        [JsonPropertyName("screenHeight")]
        public long ScreenHeight { get; set; }
    }
}
