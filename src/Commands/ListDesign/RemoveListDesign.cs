using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPListDesign")]
    [OutputType(typeof(void))]
    public class RemoveListDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantListDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var listDesigns = Identity.GetTenantListDesign(Tenant);
            if(listDesigns == null || listDesigns.Length == 0)
            {
                throw new PSArgumentException("List design provided through the Identity parameter could not be found", nameof(Identity));
            }

            foreach (var listDesign in listDesigns)
            {
                if (Force || ShouldContinue(Properties.Resources.RemoveListDesign, Properties.Resources.Confirm))
                {
                    Tenant.RemoveListDesign(listDesign.Id);
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}