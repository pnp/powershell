using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHubSite")]
    public class GetHubSite : PnPAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public HubSitePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var hubSiteProperties = Identity.GetHubSite(Tenant);
                ClientContext.Load(hubSiteProperties);
                ClientContext.ExecuteQueryRetry();
                WriteObject(hubSiteProperties);
            }
            else
            {
                var hubSitesProperties = base.Tenant.GetHubSitesProperties();
                ClientContext.Load(hubSitesProperties);
                ClientContext.ExecuteQueryRetry();
                WriteObject(hubSitesProperties, true);
            }
        }
    }
}