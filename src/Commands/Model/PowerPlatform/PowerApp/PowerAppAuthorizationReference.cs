using System.Text.Json.Serialization;
namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppAuthorizationReference
    {
        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }
    }

}
