using PnP.PowerShell.Commands.Model.Teams;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class TeamsChannelMessageReplyPipeBind
    {
        private readonly string _id;
        private readonly TeamChannelMessageReply _reply;

        public TeamsChannelMessageReplyPipeBind(string input)
        {
            _id = input;
        }

        public TeamsChannelMessageReplyPipeBind(TeamChannelMessageReply input)
        {
            _reply = input;
        }

        public string GetId()
        {
            return _reply?.Id ?? _id;
        }
    }
}
