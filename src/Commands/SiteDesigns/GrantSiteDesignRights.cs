using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsSecurity.Grant, "PnPSiteDesignRights")]
    [OutputType(typeof(void))]
    public class GrantSiteDesignRights : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline =true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Principals;

        [Parameter(Mandatory = false)]
        public TenantSiteDesignPrincipalRights Rights = TenantSiteDesignPrincipalRights.View;

        protected override void ExecuteCmdlet()
        {
            Tenant.GrantSiteDesignRights(Identity.Id, Principals, Rights);
            AdminContext.ExecuteQueryRetry();
        }
    }
}