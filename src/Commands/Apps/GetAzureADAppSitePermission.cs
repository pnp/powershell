using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADAppSitePermission", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredMinimalApiPermissions("Sites.FullControl.All")]
    public class GetPnPAzureADAppSitePermission : PnPGraphCmdlet
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
                siteId = Site.GetSiteIdThroughGraph(HttpClient, AccessToken);
            }
            else
            {
                siteId = PnPContext.Site.Id;
            }

            if (siteId != Guid.Empty)
            {
                if (!ParameterSpecified(nameof(PermissionId)))
                {
                    // all permissions
                    var results = GraphHelper.GetResultCollectionAsync<AzureADAppPermissionInternal>(HttpClient, $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/sites/{siteId}/permissions", AccessToken).GetAwaiter().GetResult();
                    if (results.Any())
                    {
                        var convertedResults = results.Select(i => i.Convert());
                        if (ParameterSpecified(nameof(AppIdentity)))
                        {
                            var filteredResults = convertedResults.Where(p => p.Apps.Any(a => a.DisplayName == AppIdentity || a.Id == AppIdentity));
                            WriteObject(filteredResults, true);
                        }
                        else
                        {
                            WriteObject(convertedResults, true);
                        }
                    }
                }
                else
                {
                    var results = GraphHelper.GetAsync<AzureADAppPermissionInternal>(HttpClient, $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{PermissionId}", AccessToken).GetAwaiter().GetResult();
                    WriteObject(results.Convert());
                }
            }
        }
    }
}