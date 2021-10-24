using System.Net.Http;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;

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

        public async Task<PlannerRoster> GetPlannerRosterAsync(HttpClient httpClient, string accessToken)
        {
            if (_roster != null)
            {
                return _roster;
            }
            return await PlannerUtility.GetRosterAsync(httpClient, accessToken, _id);
        }
    }
}
