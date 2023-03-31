using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

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
            if (PnPContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = PnPContext.Web.GetVivaDashboardAsync().GetAwaiter().GetResult();
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
