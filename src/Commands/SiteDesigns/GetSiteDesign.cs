using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteDesign")]
    public class GetSiteDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var siteDesigns = Identity.GetTenantSiteDesign(Tenant);

                if(siteDesigns == null || siteDesigns.Length == 0)
                {
                    WriteVerbose("No site designs with the identity provided through Identity have been found");
                    return;
                }

                WriteObject(siteDesigns, true);
            }
            else
            {
                var designs = Tenant.GetSiteDesigns();
                AdminContext.Load(designs);
                AdminContext.ExecuteQueryRetry();

                WriteObject(designs.ToList(), true);
            }
        }
    }
}