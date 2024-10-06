using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Remove, "PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(void))]
    public class RemoveVivaConnectionsACE : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public VivaACEPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            var pnpContext = Connection.PnPContext;
            if (pnpContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = pnpContext.Web.GetVivaDashboard();
                var aceToRemove = Identity.GetACE(dashboard, this);

                if (aceToRemove != null)
                {
                    dashboard.RemoveACE(aceToRemove.InstanceId);
                    dashboard.Save();
                }
                else
                {
                    WriteWarning("ACE with specified identifier not found");
                }
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}
