namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerPlatformConnectorPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerPlatform.Environment.PowerPlatformConnector _connector;
        public PowerPlatformConnectorPipeBind(string input)
        {
            _name = input;
        }

        public PowerPlatformConnectorPipeBind(Model.PowerPlatform.Environment.PowerPlatformConnector connector)
        {
            _connector = connector;
        }

        public string GetName()
        {
            if (_connector != null)
            {
                return _connector.Name;
            }
            return _name;
        }
    }
}
