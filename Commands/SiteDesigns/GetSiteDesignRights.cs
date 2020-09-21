using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteDesignRights", SupportsShouldProcess = true)]
    public class GetSiteDesignRights : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline =true)]
        public TenantSiteDesignPipeBind Identity;
        
        protected override void ExecuteCmdlet()
        {
            var principles = Tenant.GetSiteDesignRights(ClientContext,Identity.Id);
            ClientContext.Load(principles);
            ClientContext.ExecuteQueryRetry();
            WriteObject(principles, true);
        }
    }
}