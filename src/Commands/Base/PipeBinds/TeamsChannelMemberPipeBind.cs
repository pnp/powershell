using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TeamsChannelMemberPipeBind
    {
        private readonly string _id;
        private readonly string _userId;
        private readonly string _userUpn;
        private readonly TeamChannelMember _membership;

        public TeamsChannelMemberPipeBind()
        {
            
        }

        public TeamsChannelMemberPipeBind(string input)
        {
            if (Guid.TryParse(input, out var userId))
            {
                _userId = userId.ToString();
            }
            else if (input.Contains('@') && input.Contains('.'))
            {
                _userUpn = input;
            }
            else
            {
                _id = input;
            }
        }

        public TeamsChannelMemberPipeBind(TeamChannelMember membership)
        {
            _membership = membership;
        }

        public string GetId(Cmdlet cmdlet, PnPConnection Connection, string accessToken, string groupId, string channelId)
        {
            if (!string.IsNullOrEmpty(_id))
            {
                return _id;
            }
             
            if (_membership != null)
            {
                return _membership.Id;
            }

            var memberships = TeamsUtility.GetChannelMembers(cmdlet, Connection, accessToken, groupId, channelId);
            if (!string.IsNullOrEmpty(_userUpn))
            {
                return memberships.FirstOrDefault(m => _userUpn.Equals(m.Email, StringComparison.OrdinalIgnoreCase))?.Id;
            }

            return memberships.FirstOrDefault(m => !string.IsNullOrEmpty(m.UserId) && _userId.Equals(m.UserId, StringComparison.OrdinalIgnoreCase))?.Id;
        }

        public TeamChannelMember GetMembership(Cmdlet cmdlet, PnPConnection Connection, string accessToken, string groupId, string channelId)
        {
            if (_membership != null)
            {
                return _membership;
            }

            if (!string.IsNullOrEmpty(_id))
            {
                return TeamsUtility.GetChannelMember(cmdlet, Connection, accessToken, groupId, channelId, _id);
            }

            var memberships = TeamsUtility.GetChannelMembers(cmdlet, Connection, accessToken, groupId, channelId);
            if (!string.IsNullOrEmpty(_userUpn))
            {
                return memberships.FirstOrDefault(m => _userUpn.Equals(m.Email, StringComparison.OrdinalIgnoreCase));
            }

            return memberships.FirstOrDefault(m => !string.IsNullOrEmpty(m.UserId) && _userId.Equals(m.UserId, StringComparison.OrdinalIgnoreCase));
        }
    }
}
