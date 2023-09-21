using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    internal class AppIdentityInternal
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}