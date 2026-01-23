using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPAzureADAppSitePermission")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.FullControl.All")]
    [Alias("Revoke-PnPEntraIDAppSitePermission")]
    public class RevokePnPAzureADAppSitePermission : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PermissionId;

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            Guid siteId = Guid.Empty;
            if (ParameterSpecified(nameof(Site)))
            {
                siteId = Site.GetSiteIdThroughGraph(Connection, AccessToken);
            }
            else
            {
                siteId = new SitePipeBind(Connection.Url).GetSiteIdThroughGraph(Connection, AccessToken);
            }

            if (siteId != Guid.Empty)
            {
                if (Force || ShouldContinue("Are you sure you want to revoke the permissions?", string.Empty))
                {
                    var results = Utilities.REST.RestHelper.Delete(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{PermissionId}", AccessToken);
                }
            }
        }
    }
}