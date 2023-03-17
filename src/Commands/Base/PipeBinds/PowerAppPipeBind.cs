namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerAppPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerPlatform.PowerApp.PowerApp _powerapp;
        public PowerAppPipeBind(string input)
        {
            _name = input;
        }

        public PowerAppPipeBind(Model.PowerPlatform.PowerApp.PowerApp powerapp)
        {
            _powerapp = powerapp;
        }

        public string GetName()
        {
            if (_powerapp != null)
            {
                return _powerapp.Name;
            }
            return _name;
        }
    }
}
