using System;
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

        /// <summary>
        /// The resources whose available permissions ship with the module, with the prefix the obsolete -Scopes parameter uses for them.
        /// Permissions of any other resource are resolved from the tenant at runtime.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, (string ResourceFile, string LegacyPrefix)> curatedResources = new Dictionary<string, (string, string)>
        {
            // Graph Permissions source: https://github.com/microsoftgraph/microsoft-graph-devx-content/tree/dev/permissions
            { ResourceAppId_Graph, ("GraphPermissions.json", "MSGraph.") },
            { ResourceAppId_SPO, ("SharePointPermissions.json", "SPO.") },
            { ResourceAppID_O365Management, ("O365ManagementPermissions.json", "O365.") }
        };

        /// <summary>
        /// The permissions are immutable data embedded in the module, so they are parsed once. That keeps constructing this class cheap,
        /// which matters because callers construct it per cmdlet invocation, per dynamic parameter and per application they report on.
        /// </summary>
        private static readonly Lazy<List<PermissionScope>> allScopes = new Lazy<List<PermissionScope>>(LoadScopes);

        private static List<PermissionScope> scopes => allScopes.Value;

        /// <summary>
        /// Returns true when the available permissions of the resource ship with the module and can therefore be resolved without contacting the tenant.
        /// </summary>
        public static bool IsCuratedResource(string resourceAppId) => curatedResources.ContainsKey(resourceAppId);

        private static List<PermissionScope> LoadScopes()
        {
            var loaded = new List<PermissionScope>();
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var curatedResource in curatedResources)
            {
                using var stream = assembly.GetManifestResourceStream($"PnP.PowerShell.Commands.Resources.{curatedResource.Value.ResourceFile}");
                using var reader = new StreamReader(stream);
                var permissionsElement = JsonSerializer.Deserialize<JsonElement>(reader.ReadToEnd());
                ParseJson(loaded, "delegatedScopesList", permissionsElement, curatedResource.Key, "Scope");
                ParseJson(loaded, "applicationScopesList", permissionsElement, curatedResource.Key, "Role");
            }
            return loaded;
        }

        private static void ParseJson(List<PermissionScope> loaded, string listProperty, JsonElement permissionsElement, string resourceAppId, string type)
        {
            if (permissionsElement.TryGetProperty(listProperty, out JsonElement scopesElement))
            {
                foreach (var scope in scopesElement.EnumerateArray())
                {
                    scope.TryGetProperty("id", out JsonElement idElement);
                    scope.TryGetProperty("value", out JsonElement valueElement);
                    loaded.Add(new PermissionScope()
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
            return scopes.Select(s => curatedResources[s.resourceAppId].LegacyPrefix + s.Identifier).Distinct().ToArray();
        }

        public string[] GetIdentifiers(string resourceAppId, string type)
        {
            return scopes.Where(s => s.resourceAppId == resourceAppId && s.Type == type).Select(s => s.Identifier).Distinct().ToArray();
        }

        public PermissionScope GetScope(string resourceAppId, string identifier, string type)
        {
            return scopes.FirstOrDefault(s => s.resourceAppId == resourceAppId && s.Identifier == identifier && s.Type == type);
        }

        /// <summary>
        /// Resolves a prefixed identifier as accepted by the obsolete -Scopes parameter, for example SPO.AllSites.FullControl.
        /// As that parameter cannot express whether an application or a delegated permission is meant, an application permission takes precedence.
        /// </summary>
        public PermissionScope GetScopeByLegacyIdentifier(string identifier)
        {
            foreach (var curatedResource in curatedResources)
            {
                var prefix = curatedResource.Value.LegacyPrefix;
                if (identifier.StartsWith(prefix, StringComparison.Ordinal))
                {
                    var value = identifier.Substring(prefix.Length);
                    return GetScope(curatedResource.Key, value, "Role") ?? GetScope(curatedResource.Key, value, "Scope");
                }
            }
            return null;
        }

        public string GetIdentifier(string resourceAppId, string id, string type)
        {
            var permission = scopes.FirstOrDefault(s => s.resourceAppId == resourceAppId && s.Id == id && s.Type == type);
            if (permission != null)
            {
                return permission.Identifier;
            }
            return null;
        }
    }
}
