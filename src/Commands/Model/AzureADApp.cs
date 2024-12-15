using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    public class AzureADApp
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string DisplayName { get; set; }
        public string SignInAudience { get; set; }
        public bool? IsFallbackPublicClient { get; set; }
        public List<AppResource> RequiredResourceAccess { get; set; }

        public AppPublicClient PublicClient { get; set; }
    }

    public class AppResource
    {
        [JsonPropertyName("resourceAppId")]
        public string Id { get; set; }
        [JsonPropertyName("resourceAccess")]
        public List<PermissionScope> ResourceAccess { get; set; } = new List<PermissionScope>();
    }

    public class AppPublicClient
    {
        public string[] RedirectUris { get; set; }
    }
}