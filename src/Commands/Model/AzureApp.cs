using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    internal class AzureApp
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string DisplayName { get; set; }
        public string SignInAudience { get; set; }
    }

    public class AppResource
    {
        [JsonPropertyName("resourceAppId")]
        public string Id { get; set; }
        [JsonPropertyName("resourceAccess")]
        public List<PermissionScope> ResourceAccess { get; set; } = new List<PermissionScope>();
    }
}