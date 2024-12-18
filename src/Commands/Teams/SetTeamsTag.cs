using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsTag")]
    [RequiredApiApplicationPermissions("graph/TeamworkTag.ReadWrite")]
    public class SetTeamsTag : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTagPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId != null)
            {
                var tag = Identity.GetTag(RequestHelper, groupId);
                if (tag != null)
                {
                    if (ParameterSpecified(nameof(DisplayName)) && tag.DisplayName != DisplayName)
                    {
                        TeamsUtility.UpdateTag(RequestHelper, groupId, tag.Id, DisplayName);
                    }
                }
                else
                {
                    throw new PSArgumentException("Tag not found");
                }
            }
            else
            {
                throw new PSArgumentException("Team not found");
            }
        }
    }
}