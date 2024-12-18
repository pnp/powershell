using System.Text.Json.Serialization;
namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppReferencedResource
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
