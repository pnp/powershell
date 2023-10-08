using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteDesign")]
    [OutputType(typeof(void))]
    public class RemoveSiteDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var siteDesigns = Identity.GetTenantSiteDesign(Tenant);
            if(siteDesigns == null || siteDesigns.Length == 0)
            {
                throw new PSArgumentException("Site design provided through the Identity parameter could not be found", nameof(Identity));
            }

            foreach (var siteDesign in siteDesigns)
            {
                if (Force || ShouldContinue(Properties.Resources.RemoveSiteDesign, Properties.Resources.Confirm))
                {
                    Tenant.DeleteSiteDesign(siteDesign.Id);
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}