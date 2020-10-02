using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SiteDesign")]
    public class GetSiteDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetTenantSiteDesign(Tenant));
            }
            else
            {
                var designs = Tenant.GetSiteDesigns();
                ClientContext.Load(designs);
                ClientContext.ExecuteQueryRetry();
                WriteObject(designs.ToList(), true);
            }
        }
    }
}