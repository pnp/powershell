using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public class PowerAppUris
    {
        [JsonPropertyName("documentUri")]
        public PowerAppDocumentUri DocumentUri { get; set; }

        [JsonPropertyName("imageUris")]
        public object[] ImageUris { get; set; }

        [JsonPropertyName("additionalUris")]
        public object[] AdditionalUris { get; set; }
    }
}
