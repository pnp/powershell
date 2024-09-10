using System;
using System.Linq;
using System.Management.Automation;

using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Set, "PnPAzureADAppSitePermission")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Sites.FullControl.All")]
    [Alias("Set-PnPEntraIDAppSitePermission")]
    public class SetPnPAzureADAppSitePermission : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PermissionId;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<AzureADUpdateSitePermissionRole>))]
        public string[] Permissions;

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

                var payload = new
                {
                    roles = Permissions.Select(p => p.ToLower()).ToArray()
                };

                var results = Utilities.REST.RestHelper.Patch<AzureADAppPermissionInternal>(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{PermissionId}", AccessToken, payload);
                WriteObject(results.Convert());
            }
        }
    }
}
