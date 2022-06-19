using PnP.PowerShell.Commands.Model.Teams;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class TeamsChannelMessagePipeBind
    {
        private readonly string _id;
        private readonly TeamChannelMessage _message;

        public TeamsChannelMessagePipeBind(string input)
        {
            _id = input;
        }

        public TeamsChannelMessagePipeBind(TeamChannelMessage input)
        {
            _message = input;
        }

        public string GetId()
        {
            return _message?.Id ?? _id;
        }
    }
}
