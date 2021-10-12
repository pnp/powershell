namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerAutomateFlowPipeBind
    {
        private readonly string _name;
        private readonly Model.PowerPlatform.PowerAutomate.Flow _flow;
        public PowerAutomateFlowPipeBind(string input)
        {
            _name = input;
        }

        public PowerAutomateFlowPipeBind(Model.PowerPlatform.PowerAutomate.Flow flow)
        {
            _flow = flow;
        }

        public string GetName()
        {
            if (_flow != null)
            {
                return _flow.Name;
            }
            return _name;
        }
    }
}
