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
    [Cmdlet(VerbsSecurity.Grant, "PnPEntraIDAppListPermission")]
    [RequiredApiDelegatedPermissions("graph/Sites.FullControl.All")]
    [OutputType(typeof(AzureADAppPermission))]
    public class GrantPnPEntraIDAppListPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid AppId;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DisplayName;

        /// <summary>
        /// The list to grant permissions on. Accepts a list GUID or display name.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string List;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<AzureADNewListPermissionRole>))]
        public string[] Permissions;

        protected override void ExecuteCmdlet()
        {
            Guid siteId;
            if (ParameterSpecified(nameof(Site)))
            {
                LogDebug($"Using Microsoft Graph to look up site Id for -{nameof(Site)}");
                siteId = Site.GetSiteIdThroughGraph(Connection, AccessToken);
                LogDebug($"Site resolved to Id {siteId}");
            }
            else
            {
                LogDebug($"No -{nameof(Site)} specified, using currently connected site");
                siteId = new SitePipeBind(Connection.Url).GetSiteIdThroughGraph(Connection, AccessToken);
                LogDebug($"Currently connected site has Id {siteId}");
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

            // Apply multi-geo fix (same approach as Grant-PnPEntraIDAppSitePermission)
            Utilities.REST.RestHelper.Get(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}", AccessToken);

            var roles = Permissions.Select(p => p.ToString().ToLowerInvariant()).ToArray();

            var payload = new
            {
                grantedToV2 = new
                {
                    application = new
                    {
                        id = AppId.ToString(),
                        displayName = DisplayName
                    }
                },
                roles
            };

            LogDebug($"Granting App {AppId} the permission{(roles.Length != 1 ? "s" : "")} {string.Join(", ", roles)} on list {listId}");

            var result = Utilities.REST.RestHelper.Post<EntraIDAppListPermissionInternal>(
                Connection.HttpClient,
                $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}/lists/{listId}/permissions",
                AccessToken,
                payload);

            WriteObject(result?.Convert());
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
