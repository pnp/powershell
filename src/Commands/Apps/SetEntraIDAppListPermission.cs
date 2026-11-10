using System;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Set, "PnPEntraIDAppListPermission")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.FullControl.All")]
    [OutputType(typeof(AzureADAppPermission))]
    public class SetPnPEntraIDAppListPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PermissionId;

        /// <summary>
        /// The list whose permission should be updated. Accepts a list GUID or display name.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string List;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<AzureADUpdateListPermissionRole>))]
        public string[] Permissions;

        protected override void ExecuteCmdlet()
        {
            Guid siteId;
            if (ParameterSpecified(nameof(Site)))
            {
                LogDebug($"Using Microsoft Graph to look up site Id for -{nameof(Site)}");
                siteId = Site.GetSiteIdThroughGraph(Connection, AccessToken);
            }
            else
            {
                LogDebug($"No -{nameof(Site)} specified, using currently connected site");
                siteId = new SitePipeBind(Connection.Url).GetSiteIdThroughGraph(Connection, AccessToken);
            }

            if (siteId == Guid.Empty)
            {
                LogWarning("Unable to resolve the site Id. Ensure you pass a valid site via -Site or are connected to a site.");
                return;
            }

            var listId = ResolveListId(siteId, List);
            if (listId == Guid.Empty)
            {
                LogWarning($"Unable to resolve list '{List}' on site {siteId}. Ensure the list exists and you have access.");
                return;
            }

            var payload = new
            {
                roles = Permissions.Select(p => p.ToLowerInvariant()).ToArray()
            };

            // Strip any whitespace inadvertently included when copying a line-wrapped terminal value.
            var cleanPermissionId = Uri.EscapeDataString(PermissionId.Trim().Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", ""));
            LogDebug($"Updating permission {cleanPermissionId} on list {listId} to {string.Join(", ", payload.roles)}");

            var result = Utilities.REST.RestHelper.Patch<EntraIDAppListPermissionInternal>(
                Connection.HttpClient,
                $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}/lists/{listId}/permissions/{cleanPermissionId}",
                AccessToken,
                payload);

            if (result != null)
            {
                var converted = result.Convert();
                EnrichWithDisplayNames(converted);
                WriteObject(converted);
            }
        }

        /// <summary>
        /// Best-effort resolution of missing app display names via Entra ID service principals.
        /// The Graph beta API does not return displayName in grantedToV2.application on PATCH responses.
        /// Silently skips if the caller lacks Application.Read.All / Directory.Read.All.
        /// </summary>
        private void EnrichWithDisplayNames(AzureADAppPermission permission)
        {
            if (permission?.Apps == null) return;

            foreach (var app in permission.Apps)
            {
                if (!string.IsNullOrEmpty(app.DisplayName) || string.IsNullOrEmpty(app.Id))
                    continue;

                try
                {
                    var raw = Utilities.REST.RestHelper.Get(
                        Connection.HttpClient,
                        $"https://{Connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=appId eq '{Uri.EscapeDataString(app.Id)}'&$select=displayName,appId",
                        AccessToken);

                    if (string.IsNullOrEmpty(raw)) continue;

                    var doc = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(raw);
                    if (doc.TryGetProperty("value", out var valueEl))
                    {
                        var first = valueEl.EnumerateArray().FirstOrDefault();
                        if (first.ValueKind == System.Text.Json.JsonValueKind.Object &&
                            first.TryGetProperty("displayName", out var nameEl))
                        {
                            app.DisplayName = nameEl.GetString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogDebug($"Could not resolve display name for app {app.Id}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Resolves the list identifier (GUID or display name) to a list GUID via the Graph API.
        /// </summary>
        private Guid ResolveListId(Guid siteId, string listIdentifier)
        {
            if (Guid.TryParse(listIdentifier, out Guid parsedId))
            {
                return parsedId;
            }

            LogDebug($"List identifier '{listIdentifier}' is not a GUID; querying Graph to resolve list by display name");

            var raw = Utilities.REST.RestHelper.Get(
                Connection.HttpClient,
                $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}/lists?$select=id,displayName",
                AccessToken);

            if (string.IsNullOrEmpty(raw))
            {
                return Guid.Empty;
            }

            var doc = JsonSerializer.Deserialize<JsonElement>(raw);
            if (doc.TryGetProperty("value", out JsonElement valueElement))
            {
                foreach (var item in valueElement.EnumerateArray())
                {
                    if (item.TryGetProperty("displayName", out JsonElement displayNameEl) &&
                        displayNameEl.GetString().Equals(listIdentifier, StringComparison.OrdinalIgnoreCase))
                    {
                        if (item.TryGetProperty("id", out JsonElement idEl))
                        {
                            return Guid.Parse(idEl.GetString());
                        }
                    }
                }
            }

            return Guid.Empty;
        }
    }
}
