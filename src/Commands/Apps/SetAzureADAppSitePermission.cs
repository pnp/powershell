using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Set, "PnPAzureADAppSitePermission")]
    [RequiredMinimalApiPermissions("Sites.FullControl.All")]
    public class SetPnPAzureADAppSitePermission : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PermissionId;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        [ValidateSet("Write", "Read", "Manage", "FullControl")]
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

                var results = PnP.PowerShell.Commands.Utilities.REST.RestHelper.PatchAsync<AzureADAppPermissionInternal>(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{PermissionId}", AccessToken, payload).GetAwaiter().GetResult();
                WriteObject(results.Convert());
            }
        }
    }
}
