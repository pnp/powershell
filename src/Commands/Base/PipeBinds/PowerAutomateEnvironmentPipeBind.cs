namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerPlatformEnvironmentPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerPlatform.Environment.Environment _environment;
        public PowerPlatformEnvironmentPipeBind(string input)
        {
            _name = input;
        }

        public PowerPlatformEnvironmentPipeBind(Model.PowerPlatform.Environment.Environment environment)
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
