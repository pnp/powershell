using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerRosterPlan", DefaultParameterSetName = ParameterSet_BYROSTER)]
    [RequiredMinimalApiPermissions("Tasks.Read")]
    public class GetPlannerRosterPlan : PnPGraphCmdlet
    {
        private const string ParameterSet_BYUSER = "Get by user";
        private const string ParameterSet_BYROSTER = "Get by roster";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYUSER)]
        public string User;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYROSTER)]
        public PlannerRosterPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case ParameterSet_BYUSER:
                    WriteObject(PlannerUtility.GetRosterPlansByUserAsync(this, Connection, AccessToken, User).GetAwaiter().GetResult(), true);
                    break;
                    
                case ParameterSet_BYROSTER:
                    var plannerRoster = Identity.GetPlannerRosterAsync(this, Connection, AccessToken).GetAwaiter().GetResult();
                    if (plannerRoster == null)
                    {
                        throw new PSArgumentException($"Planner Roster provided through {nameof(Identity)} could not be found", nameof(Identity));
                    }
                    WriteObject(PlannerUtility.GetRosterPlansByRosterAsync(this, Connection, AccessToken, plannerRoster.Id).GetAwaiter().GetResult(), true);
                    break;
            }
        }
    }
}