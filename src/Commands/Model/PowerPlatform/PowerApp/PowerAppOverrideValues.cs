using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppOverrideValues
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

}
