using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TeamsTagPipeBind
    {
        private readonly string _id;
        public TeamsTagPipeBind()
        {
        }

        public TeamsTagPipeBind(string input)
        {
            _id = input;
        }

        public TeamsTagPipeBind(TeamTag tag)
        {
            _id = tag.Id;
        }


        public TeamTag GetTag(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupId)
        {
            var tags = TeamsUtility.GetTagsAsync(cmdlet, accessToken, connection, groupId).GetAwaiter().GetResult();
            if (tags != null && tags.Any())
            {
                return tags.FirstOrDefault(c => c.Id.Equals(_id, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

    }
}
