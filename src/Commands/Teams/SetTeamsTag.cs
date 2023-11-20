﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsTag")]
    [RequiredMinimalApiPermissions("TeamworkTag.ReadWrite")]
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
            var groupId = Team.GetGroupId(Connection, AccessToken);
            if (groupId != null)
            {
                var tag = Identity.GetTag(Connection, AccessToken, groupId);
                if (tag != null)
                {
                    if (ParameterSpecified(nameof(DisplayName)) && tag.DisplayName != DisplayName)
                    {
                        TeamsUtility.UpdateTagAsync(Connection, AccessToken, groupId, tag.Id, DisplayName).GetAwaiter().GetResult();
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