using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    internal class PermissionIdentityInternal
    {
        [JsonPropertyName("application")]
        public AppIdentityInternal Application { get; set; }
    }
}