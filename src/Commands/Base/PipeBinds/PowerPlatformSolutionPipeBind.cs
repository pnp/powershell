namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerPlatformSolutionPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerPlatform.Environment.Solution.PowerPlatformSolution _solution;
        public PowerPlatformSolutionPipeBind(string input)
        {
            _name = input;
        }

        public PowerPlatformSolutionPipeBind(Model.PowerPlatform.Environment.Solution.PowerPlatformSolution solution)
        {
            _solution = solution;
        }

        public string GetName()
        {
            if (_solution != null)
            {
                return _solution.FriendlyName;
            }
            return _name;
        }
    }
}
