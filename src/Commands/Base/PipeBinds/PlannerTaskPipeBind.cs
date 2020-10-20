using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PlannerTaskPipeBind
    {
        private readonly string _id;

        public PlannerTaskPipeBind(string input)
        {
            _id = input;
        }

        public PlannerTaskPipeBind(Model.Planner.PlannerTask task)
        {
            _id = task.Id;
        }

        public string Id => _id;

    }
}
