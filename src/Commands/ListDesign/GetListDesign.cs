using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPListDesign")]
    [OutputType(typeof(TenantListDesign))]
    public class GetListDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantListDesignPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var listDesigns = Identity.GetTenantListDesign(Tenant);

                if(listDesigns.Length == 0)
                {
                    WriteVerbose($"No list designs with the identity provided through {nameof(Identity)} have been found");
                }

                WriteObject(listDesigns, true);
            }
            else
            {
                var designs = Tenant.GetListDesigns();
                AdminContext.Load(designs);
                AdminContext.ExecuteQueryRetry();

                WriteObject(designs, true);
            }
        }
    }
}