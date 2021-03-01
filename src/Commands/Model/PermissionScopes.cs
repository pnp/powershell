using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Model
{
    public class PermissionScopes
    {
        public static string ResourceAppId_Graph = "00000003-0000-0000-c000-000000000000";
        public static string ResourceAppId_SPO = "00000003-0000-0ff1-ce00-000000000000";
        public static string ResourceAppID_O365Management = "c5393580-f805-4401-95e8-94b7a6ef2fc2";
        private List<PermissionScope> scopes = new List<PermissionScope>();
        public PermissionScopes()
        {
            // Graph Permissions source: https://github.com/microsoftgraph/microsoft-graph-devx-content/tree/dev/permissions

            var assembly = Assembly.GetExecutingAssembly();
            var graphResourceName = "PnP.PowerShell.Commands.Resources.GraphPermissions.json";
            using (var stream = assembly.GetManifestResourceStream(graphResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var permissionsJson = reader.ReadToEnd();
                    var permissionsElement = JsonSerializer.Deserialize<JsonElement>(permissionsJson);
                    ParseJson("delegatedScopesList", permissionsElement, ResourceAppId_Graph, "Scope");
                    ParseJson("applicationScopesList", permissionsElement, ResourceAppId_Graph, "Role");
                }
            }
            var spoResourceName = "PnP.PowerShell.Commands.Resources.SharePointPermissions.json";
            using (var stream = assembly.GetManifestResourceStream(spoResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var permissionsJson = reader.ReadToEnd();
                    var permissionsElement = JsonSerializer.Deserialize<JsonElement>(permissionsJson);
                    ParseJson("delegatedScopesList", permissionsElement, ResourceAppId_SPO, "Scope");
                    ParseJson("applicationScopesList", permissionsElement, ResourceAppId_SPO, "Role");
                }
            }
            var O365MgtResourceName = "PnP.PowerShell.Commands.Resources.O365ManagementPermissions.json";
            using (var stream = assembly.GetManifestResourceStream(O365MgtResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var permissionsJson = reader.ReadToEnd();
                    var permissionsElement = JsonSerializer.Deserialize<JsonElement>(permissionsJson);
                    ParseJson("delegatedScopesList", permissionsElement, ResourceAppID_O365Management, "Scope");
                    ParseJson("applicationScopesList", permissionsElement, ResourceAppID_O365Management, "Role");
                }
            }
        }

        private void ParseJson(string listProperty, JsonElement permissionsElement, string resourceAppId, string type)
        {
            if (permissionsElement.TryGetProperty(listProperty, out JsonElement scopesElement))
            {
                foreach (var scope in scopesElement.EnumerateArray())
                {
                    scope.TryGetProperty("id", out JsonElement idElement);
                    scope.TryGetProperty("value", out JsonElement valueElement);
                    scopes.Add(new PermissionScope()
                    {
                        resourceAppId = resourceAppId,
                        Id = idElement.GetString(),
                        Identifier = valueElement.GetString(),
                        Type = type
                    });
                }
            }
        }

        public string[] GetIdentifiers()
        {

            return this.scopes.Select(s => (s.resourceAppId == ResourceAppId_SPO ? "SPO." : s.resourceAppId == ResourceAppId_Graph ? "MSGraph." : "O365.") + s.Identifier).ToArray();
        }

        public string[] GetIdentifiers(string resourceAppId, string type)
        {
            return this.scopes.Where(s => s.resourceAppId == resourceAppId && s.Type == type).Select(s => s.Identifier).ToArray();
        }

        public PermissionScope GetScope(string identifier)
        {
            return this.scopes.FirstOrDefault(s => s.Identifier == identifier);
        }

        public PermissionScope GetScope(string resourceAppId, string identifier, string type)
        {
            return this.scopes.FirstOrDefault(s => s.resourceAppId == resourceAppId && s.Identifier == identifier && s.Type == type);
        }

        public string GetIdentifier(string resourceAppId, string id, string type)
        {
            var permission = this.scopes.FirstOrDefault(s => s.resourceAppId == resourceAppId && s.Id == id && s.Type == type);
            if (permission != null)
            {
                return permission.Identifier;
            }
            return null;
        }
    }
}
