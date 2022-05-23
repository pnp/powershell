using PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PowerAutomateFlowRunPipeBind
    {
        private readonly string _name;
        private readonly FlowRun _flowRun;
        public PowerAutomateFlowRunPipeBind(string input)
        {
            _name = input;
        }

        public PowerAutomateFlowRunPipeBind(FlowRun flowRun)
        {
            _flowRun = flowRun;
        }

        public string GetName()
        {
            return _flowRun != null ? _flowRun.Name : _name;
        }
    }
}
