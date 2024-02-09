using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TeamsChannelPipeBind
    {
        private readonly string _id;
        private readonly string _displayName;
        public TeamsChannelPipeBind()
        {

        }

        public TeamsChannelPipeBind(string input)
        {
            // check if it's a channel id
            if (input.Contains("@thread.") && input.Substring(2, 1) == ":")
            {
                _id = input;
            }
            else
            {
                _displayName = input;
            }
        }

        public TeamsChannelPipeBind(Model.Teams.TeamChannel channel)
        {
            _id = channel.Id;
        }


        public string Id => _id;

        public string GetId(PnPConnection connection, string accessToken, string groupId)
        {
            if (!string.IsNullOrEmpty(_id))
            {
                return _id;
            }
            else
            {
                var channels = TeamsUtility.GetChannelsAsync(accessToken, connection, groupId).GetAwaiter().GetResult();
                return channels.FirstOrDefault(c => c.DisplayName.Equals(_displayName, StringComparison.OrdinalIgnoreCase))?.Id;
            }
        }

        public TeamChannel GetChannel(PnPConnection connection, string accessToken, string groupId, bool useBeta = false)
        {
            if (!string.IsNullOrEmpty(_id))
            {
                var channel = TeamsUtility.GetChannelAsync(accessToken, connection, groupId, _id, useBeta).GetAwaiter().GetResult();
                return channel;
            }
            else
            {
                var channels = TeamsUtility.GetChannelsAsync(accessToken, connection, groupId, useBeta).GetAwaiter().GetResult();
                if (channels != null && channels.Any())
                {
                    return channels.FirstOrDefault(c => c.DisplayName.Equals(_displayName, StringComparison.OrdinalIgnoreCase));
                }
                return null;
            }
        }
    }
}
