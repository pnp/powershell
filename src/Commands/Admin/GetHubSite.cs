using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHubSite")]
    public class GetHubSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public HubSitePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var hubSiteProperties = Identity.GetHubSite(Tenant);
                AdminContext.Load(hubSiteProperties);
                AdminContext.ExecuteQueryRetry();
                if ((bool)hubSiteProperties.ServerObjectIsNull)
                {
                    WriteObject(null);
                }
                else { 
                    WriteObject(hubSiteProperties);
                }
            }
            else
            {
                var hubSitesProperties = base.Tenant.GetHubSitesProperties();
                AdminContext.Load(hubSitesProperties);
                AdminContext.ExecuteQueryRetry();
                WriteObject(hubSitesProperties, true);
            }
        }
    }
}