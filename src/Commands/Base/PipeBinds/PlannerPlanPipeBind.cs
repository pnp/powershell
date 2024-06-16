using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PlannerPlanPipeBind
    {
        private readonly string _id;
        private readonly PlannerPlan _plan;
        public PlannerPlanPipeBind()
        {
        }

        public PlannerPlanPipeBind(string input)
        {
            _id = input;
        }

        public PlannerPlanPipeBind(PlannerPlan plan)
        {
            _plan = plan;
        }

        public PlannerPlan GetPlan(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupId, bool resolveIdentities)
        {
            if (_plan != null)
            {
                return _plan;
            }
            // first try to get the plan by id
            try
            {
                return PlannerUtility.GetPlan(cmdlet, connection, accessToken, _id, resolveIdentities);
            }
            catch (GraphException)
            {
                var plans = PlannerUtility.GetPlans(cmdlet,connection, accessToken, groupId, resolveIdentities);
                if (plans != null && plans.Any())
                {
                    var collection = plans.Where(p => p.Title.Equals(_id));
                    if (collection != null && collection.Any() && collection.Count() == 1)
                    {
                        return collection.First();
                    }
                    else
                    {
                        throw new PSArgumentException("More than one plan with the same title found. Use Get-PnPPlannerPlan to list all plans.");
                    }
                }
            }
            return null;
        }

        public string GetId(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupId)
        {
            if (_plan != null)
            {
                return _plan.Id;
            }
            // first try to get the plan by id
            try
            {
                var plan = PlannerUtility.GetPlan(cmdlet, connection, accessToken, _id, false);
                return plan.Id;
            }
            catch (GraphException)
            {
                var plans = PlannerUtility.GetPlans(cmdlet, connection, accessToken, groupId, false);
                if (plans != null && plans.Any())
                {
                    var collection = plans.Where(p => p.Title.Equals(_id));
                    if (collection != null && collection.Any() && collection.Count() == 1)
                    {
                        return collection.First().Id;
                    }
                    else
                    {
                        throw new PSArgumentException("More than one plan with the same title found. Use Get-PnPPlannerPlan to list all plans.");
                    }
                }
            }
            return null;
        }
    }
}
