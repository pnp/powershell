using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.EntraID;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDAppSitePermission", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredMinimalApiPermissions("Sites.FullControl.All")]
    [Alias("Get-PnPAzureADAppSitePermission")]
    public class GetPnPEntraIDAppSitePermission : PnPGraphCmdlet
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

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PERMISSIONID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            Guid siteId = Guid.Empty;
            if (ParameterSpecified(nameof(Site)))
            {
                siteId = Site.GetSiteIdThroughGraph(Connection, AccessToken);
            }
            else
            {
                siteId = PnPContext.Site.Id;
            }

            if (siteId != Guid.Empty)
            {
                if (!ParameterSpecified(nameof(PermissionId)))
                {
                    // Cache the access token so it will not be requested for every following request in this cmdlet
                    var accessToken = AccessToken;

                    // All permissions, first fetch just the Ids as the API works in a weird way that requesting all permissions does not reveal their roles, so we will request all permissions and then request each permission individually so we will also have the roles
                    var permissions = GraphHelper.GetResultCollectionAsync<AppPermissionInternal>(Connection, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions?$select=Id", accessToken).GetAwaiter().GetResult();
                    if (permissions.Any())
                    {
                        var results = new List<AppPermission>();
                        foreach (var permission in permissions)
                        {
                            // Request the permission individually so it will include the roles
                            var detailedApp = GraphHelper.GetAsync<AppPermissionInternal>(Connection, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{permission.Id}", accessToken).GetAwaiter().GetResult();
                            results.Add(detailedApp.Convert());
                        }

                        if (ParameterSpecified(nameof(AppIdentity)))
                        {
                            var filteredResults = results.Where(p => p.Apps.Any(a => a.DisplayName == AppIdentity || a.Id == AppIdentity));
                            WriteObject(filteredResults, true);
                        }
                        else
                        {
                            WriteObject(results, true);
                        }
                    }
                }
                else
                {
                    var results = GraphHelper.GetAsync<AppPermissionInternal>(Connection, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{PermissionId}", AccessToken).GetAwaiter().GetResult();
                    WriteObject(results.Convert());
                }
            }
        }
    }
}