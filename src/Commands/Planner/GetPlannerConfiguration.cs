using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerConfiguration")]
    [RequiredApiApplicationPermissions("https://tasks.office.com/.default")]
    public class GetPlannerConfiguration : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.GetPlannerConfig(RequestHelper);
            WriteObject(result);
        }
    }
}