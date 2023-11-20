namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerPlatformCustomConnectorPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerPlatform.Environment.PowerPlatformConnector _connector;
        public PowerPlatformCustomConnectorPipeBind(string input)
        {
            _name = input;
        }

        public PowerPlatformCustomConnectorPipeBind(Model.PowerPlatform.Environment.PowerPlatformConnector connector)
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
