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
