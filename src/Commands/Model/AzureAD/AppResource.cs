using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AppResource
    {
        [JsonPropertyName("resourceAppId")]
        public string Id { get; set; }
        [JsonPropertyName("resourceAccess")]
        public List<PermissionScope> ResourceAccess { get; set; } = new List<PermissionScope>();
    }
}