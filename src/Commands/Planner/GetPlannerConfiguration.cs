using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerConfiguration")]
    [RequiredApiDelegatedOrApplicationPermissions("https://tasks.office.com/.default")]
    public class GetPlannerConfiguration : PnPTasksCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.GetPlannerConfig(RequestHelper);
            WriteObject(result);
        }
    }
}