using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDAppFilePermission", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.FullControl.All")]
    [OutputType(typeof(AzureADAppPermission))]
    public class GetPnPEntraIDAppFilePermission : PnPGraphCmdlet
    {
        private const string ParameterSet_ALL = "All Permissions";
        private const string ParameterSet_PERMISSIONID = "By Permission Id";
        private const string ParameterSet_APPIDENTITY = "By App Display Name or App Id";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_PERMISSIONID)]
        [ValidateNotNullOrEmpty]
        public string PermissionId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPIDENTITY)]
        [ValidateNotNullOrEmpty]
        public string AppIdentity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ALL)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_PERMISSIONID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPIDENTITY)]
        [ValidateNotNullOrEmpty]
        public string List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PERMISSIONID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPIDENTITY)]
        [ValidateNotNullOrEmpty]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PERMISSIONID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPIDENTITY)]
        [ValidateNotNullOrEmpty]
        public string FileId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PERMISSIONID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPIDENTITY)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            if (!ParameterSpecified(nameof(Path)) && !ParameterSpecified(nameof(FileId)))
            {
                ThrowTerminatingError(new ErrorRecord(
                    new PSArgumentException("Either -Path or -FileId must be specified."),
                    "MissingFileIdentifier", ErrorCategory.InvalidArgument, null));
                return;
            }

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

            var driveId = ResolveDriveId(siteId, listId);
            if (string.IsNullOrEmpty(driveId))
            {
                LogWarning($"Unable to resolve the drive for list '{List}'. Ensure the list is a document library.");
                return;
            }

            string driveItemId;
            if (ParameterSpecified(nameof(FileId)))
            {
                driveItemId = FileId;
                LogDebug($"Using provided -{nameof(FileId)} directly as drive item Id");
            }
            else
            {
                driveItemId = ResolveDriveItemId(driveId, Path);
                if (string.IsNullOrEmpty(driveItemId))
                {
                    LogWarning($"Unable to resolve file at path '{Path}' in drive {driveId}. Ensure the path is correct and relative to the library root.");
                    return;
                }
            }

            if (ParameterSpecified(nameof(PermissionId)))
            {
                var cleanPermissionId = Uri.EscapeDataString(PermissionId.Trim().Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", ""));
                var result = GraphRequestHelper.Get<EntraIDAppDrivePermissionInternal>($"beta/drives/{driveId}/items/{driveItemId}/permissions/{cleanPermissionId}");
                if (result != null)
                {
                    var converted = result.Convert();
                    EnrichWithDisplayNames(converted);
                    WriteObject(converted);
                }
            }
            else
            {
                var permissions = GraphRequestHelper.GetResultCollection<EntraIDAppDrivePermissionInternal>($"beta/drives/{driveId}/items/{driveItemId}/permissions?$select=id");
                if (permissions != null && permissions.Any())
                {
                    var results = new List<AzureADAppPermission>(permissions.Count());
                    foreach (var permission in permissions)
                    {
                        var detailed = GraphRequestHelper.Get<EntraIDAppDrivePermissionInternal>($"beta/drives/{driveId}/items/{driveItemId}/permissions/{permission.Id}");
                        if (detailed != null)
                        {
                            var converted = detailed.Convert();
                            EnrichWithDisplayNames(converted);
                            results.Add(converted);
                        }
                    }

                    if (ParameterSpecified(nameof(AppIdentity)))
                    {
                        var filtered = results.Where(p => p.Apps.Any(a => a.DisplayName == AppIdentity || a.Id == AppIdentity));
                        WriteObject(filtered, true);
                    }
                    else
                    {
                        WriteObject(results, true);
                    }
                }
            }
        }

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

                    var doc = JsonSerializer.Deserialize<JsonElement>(raw);
                    if (doc.TryGetProperty("value", out JsonElement valueEl))
                    {
                        var first = valueEl.EnumerateArray().FirstOrDefault();
                        if (first.ValueKind == JsonValueKind.Object &&
                            first.TryGetProperty("displayName", out JsonElement nameEl))
                        {
                            app.DisplayName = nameEl.GetString();
                            LogDebug($"Resolved display name '{app.DisplayName}' for app {app.Id}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogDebug($"Could not resolve display name for app {app.Id}: {ex.Message}");
                }
            }
        }

        private Guid ResolveListId(Guid siteId, string listIdentifier)
        {
            if (Guid.TryParse(listIdentifier, out Guid parsedId))
                return parsedId;

            LogDebug($"List identifier '{listIdentifier}' is not a GUID; querying Graph to resolve by display name");

            var raw = Utilities.REST.RestHelper.Get(
                Connection.HttpClient,
                $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}/lists?$select=id,displayName",
                AccessToken);

            if (string.IsNullOrEmpty(raw)) return Guid.Empty;

            var doc = JsonSerializer.Deserialize<JsonElement>(raw);
            if (doc.TryGetProperty("value", out JsonElement valueEl))
            {
                foreach (var item in valueEl.EnumerateArray())
                {
                    if (item.TryGetProperty("displayName", out JsonElement nameEl) &&
                        nameEl.GetString().Equals(listIdentifier, StringComparison.OrdinalIgnoreCase) &&
                        item.TryGetProperty("id", out JsonElement idEl))
                    {
                        return Guid.Parse(idEl.GetString());
                    }
                }
            }

            return Guid.Empty;
        }

        private string ResolveDriveId(Guid siteId, Guid listId)
        {
            LogDebug($"Resolving drive Id for list {listId} on site {siteId}");

            var raw = Utilities.REST.RestHelper.Get(
                Connection.HttpClient,
                $"https://{Connection.GraphEndPoint}/beta/sites/{siteId}/lists/{listId}/drive?$select=id",
                AccessToken);

            if (string.IsNullOrEmpty(raw)) return null;

            var doc = JsonSerializer.Deserialize<JsonElement>(raw);
            if (doc.TryGetProperty("id", out JsonElement idEl))
                return idEl.GetString();

            return null;
        }

        private string ResolveDriveItemId(string driveId, string path)
        {
            var encodedPath = string.Join("/", path.Trim('/').Split('/').Select(Uri.EscapeDataString));
            LogDebug($"Resolving drive item Id for path '{path}' in drive {driveId}");

            var raw = Utilities.REST.RestHelper.Get(
                Connection.HttpClient,
                $"https://{Connection.GraphEndPoint}/beta/drives/{driveId}/root:/{encodedPath}?$select=id",
                AccessToken);

            if (string.IsNullOrEmpty(raw)) return null;

            var doc = JsonSerializer.Deserialize<JsonElement>(raw);
            if (doc.TryGetProperty("id", out JsonElement idEl))
                return idEl.GetString();

            return null;
        }
    }
}
