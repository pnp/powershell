using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPSiteDesignRights")]
    [OutputType(typeof(void))]
    public class RevokeSiteDesignRights : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline =true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Principals;

        protected override void ExecuteCmdlet()
        {
            Tenant.RevokeSiteDesignRights(AdminContext, Identity.Id, Principals);
            AdminContext.ExecuteQueryRetry();
        }
    }
}