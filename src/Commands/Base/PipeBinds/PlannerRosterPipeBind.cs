using System.Management.Automation;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PlannerRosterPipeBind
    {
        private readonly string _id;
        private readonly PlannerRoster _roster;
        public PlannerRosterPipeBind()
        {
        }

        public PlannerRosterPipeBind(string input)
        {
            _id = input;
        }

        public PlannerRosterPipeBind(PlannerRoster roster)
        {
            _roster = roster;
        }

        public PlannerRoster GetPlannerRoster(ApiRequestHelper requestHelper)
        {
            if (_roster != null)
            {
                return _roster;
            }
            return PlannerUtility.GetRoster(requestHelper, _id);
        }
    }
}
