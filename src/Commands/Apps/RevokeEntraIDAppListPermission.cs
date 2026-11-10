using System;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPEntraIDAppListPermission")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.FullControl.All")]
    public class RevokePnPEntraIDAppListPermission : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PermissionId;

        /// <summary>
        /// The list from which the permission should be revoked. Accepts a list GUID or display name.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string List;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

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

            if (Force || ShouldContinue("Are you sure you want to revoke the list permission?", string.Empty))
            {
                // PermissionId is a long base64 string; strip any whitespace the user may have
                // inadvertently included when copying a line-wrapped terminal value.
                var cleanPermissionId = Uri.EscapeDataString(PermissionId.Trim().Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", ""));
                LogDebug($"Revoking permission {cleanPermissionId} from list {listId} on site {siteId}");
                Utilities.REST.RestHelper.Delete(
                    Connection.HttpClient,
                    $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}/lists/{listId}/permissions/{cleanPermissionId}",
                    AccessToken);
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
