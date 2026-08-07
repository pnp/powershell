
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTag")]
    [RequiredApiDelegatedPermissions("graph/TeamworkTag.Read")]
    [RequiredApiDelegatedPermissions("graph/TeamworkTag.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/TeamworkTag.Read.All")]
    [RequiredApiApplicationPermissions("graph/TeamworkTag.ReadWrite.All")]
    public class GetTeamsTag : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public TeamsTagPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(GraphRequestHelper);
            if (string.IsNullOrEmpty(groupId))
            {
                throw new PSArgumentException("Team not found");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var tags = Identity.GetTag(GraphRequestHelper, groupId);
                WriteObject(tags, false);
            }
            else
            {
                var tags = TeamsUtility.GetTags(GraphRequestHelper, groupId);
                WriteObject(tags, true);
            }
        }
    }
}