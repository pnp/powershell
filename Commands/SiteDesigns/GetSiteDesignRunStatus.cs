using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.SiteDesigns
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteDesignRunStatus")]
    public class GetSiteDesignRunStatus : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public TenantSiteDesignRun Run;

        protected override void ExecuteCmdlet()
        {
            var status = Tenant.GetSiteDesignRunStatus(Run.SiteId, Run.WebId, Run.Id);
            Tenant.Context.Load(status);
            Tenant.Context.ExecuteQueryRetry();
            WriteObject(status, true);
        }
    }
}