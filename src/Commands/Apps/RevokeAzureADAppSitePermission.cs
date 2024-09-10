using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPAzureADAppSitePermission")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Sites.FullControl.All")]
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
                siteId = PnPContext.Site.Id;
            }

            if (siteId != Guid.Empty)
            {
                if (Force || ShouldContinue("Are you sure you want to revoke the permissions?", string.Empty))
                {
                    var results = PnP.PowerShell.Commands.Utilities.REST.RestHelper.Delete(Connection.HttpClient, $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/permissions/{PermissionId}", AccessToken);
                }
            }
        }
    }
}