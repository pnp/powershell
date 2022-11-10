using PnP.Core.Model.SharePoint;

using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Get, "PnPVivaConnectionsDashboardACE")]
    [OutputType(typeof(AdaptiveCardExtension))]
    public class GetVivaConnectionsDashboard : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public Guid Identity;
        protected override void ExecuteCmdlet()
        {
            if (PnPContext.Site.IsHomeSite())
            {
                IVivaDashboard dashboard = PnPContext.Web.GetVivaDashboardAsync().GetAwaiter().GetResult();

                if (ParameterSpecified(nameof(Identity)))
                {
                    var aceToRetrieve = dashboard.ACEs.FirstOrDefault(p => p.InstanceId == Identity);
                    if (aceToRetrieve != null)
                    {
                        WriteObject(aceToRetrieve);
                    }
                    else
                    {
                        WriteWarning("ACE with specified Instance Id not found");
                    }
                }
                else
                {
                    WriteObject(dashboard.ACEs, true);
                }
            }
            else
            {
                WriteWarning("Connected site is not a home site");
            }
        }
    }
}
