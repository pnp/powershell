using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteDesignRights")]
    [OutputType(typeof(ClientObjectList<TenantSiteDesignPrincipal>))]
    public class GetSiteDesignRights : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline =true)]
        public TenantSiteDesignPipeBind Identity;
        
        protected override void ExecuteCmdlet()
        {
            var principles = Tenant.GetSiteDesignRights(AdminContext,Identity.Id);
            AdminContext.Load(principles);
            AdminContext.ExecuteQueryRetry();
            WriteObject(principles, true);
        }
    }
}