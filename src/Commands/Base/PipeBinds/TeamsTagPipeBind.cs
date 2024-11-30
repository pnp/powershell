using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

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


        public TeamTag GetTag(GraphHelper requestHelper, string groupId)
        {
            var tags = TeamsUtility.GetTags(requestHelper, groupId);
            if (tags != null && tags.Any())
            {
                return tags.FirstOrDefault(c => c.Id.Equals(_id, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

    }
}
