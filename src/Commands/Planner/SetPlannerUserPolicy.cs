using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Set, "PnPPlannerUserPolicy")]
    [RequiredApiApplicationPermissions("https://tasks.office.com/.default")]
    public class SetPlannerUserPolicy : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public bool? BlockDeleteTasksNotCreatedBySelf;

        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.SetPlannerUserPolicy(RequestHelper, Identity, BlockDeleteTasksNotCreatedBySelf);
            WriteObject(result);
        }
    }
}