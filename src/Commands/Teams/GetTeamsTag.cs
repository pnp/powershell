
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTag")]
    [RequiredMinimalApiPermissions("TeamworkTag.Read")]
    public class GetTeamsTag : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            var group = Team.GetGroupId(Connection, AccessToken);
            if (string.IsNullOrEmpty(group))
            {
                throw new PSArgumentException("Team not found");
            }

            if (!string.IsNullOrEmpty(Identity))
            {
                var tags = TeamsUtility.GetTagsWithIdAsync(AccessToken, Connection, group, Identity).GetAwaiter().GetResult();
                WriteObject(tags, false);
            }
            else
            {
                var tags = TeamsUtility.GetTagsAsync(AccessToken, Connection, group).GetAwaiter().GetResult();
                WriteObject(tags, true);
            }
        }
    }
}