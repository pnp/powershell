using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Get, "PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(AdaptiveCardExtension))]
    public class GetVivaConnectionsDashboard : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public VivaACEPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            var pnpContext = Connection.PnPContext;

            IVivaDashboard dashboard = pnpContext.Web.GetVivaDashboard();
            if (dashboard == null)
            {
                LogError("Viva Connections dashboard not found. Create or configure the Viva Connections dashboard page (Dashboard.aspx) on the connected SharePoint Team site or Communication site.");
                return;
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var aceToRetrieve = Identity.GetACE(dashboard, this);
                if (aceToRetrieve != null)
                {
                    WriteObject(aceToRetrieve);
                }
                else
                {
                    LogWarning("ACE with specified identifier not found");
                }
            }
            else
            {
                WriteObject(dashboard.ACEs, true);
            }
        }
    }
}
