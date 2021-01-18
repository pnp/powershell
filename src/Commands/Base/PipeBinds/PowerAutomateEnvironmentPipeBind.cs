namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerAutomateEnvironmentPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerAutomate.Environment _environment;
        public PowerAutomateEnvironmentPipeBind(string input)
        {
            _name = input;
        }

        public PowerAutomateEnvironmentPipeBind(Model.PowerAutomate.Environment environment)
        {
            _environment = environment;
        }


        public string GetName()
        {
            if (_environment != null)
            {
                return _environment.Name;
            }
            return _name;
        }

    }
}
