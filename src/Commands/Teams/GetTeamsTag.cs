﻿
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTag")]
    [RequiredMinimalApiPermissions("TeamworkTag.Read")]
    public class GetTeamsTag : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public TeamsTagPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(Connection, AccessToken);
            if (string.IsNullOrEmpty(groupId))
            {
                throw new PSArgumentException("Team not found");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var tags = Identity.GetTag(Connection, AccessToken, groupId);
                WriteObject(tags, false);
            }
            else
            {
                var tags = TeamsUtility.GetTagsAsync(AccessToken, Connection, groupId).GetAwaiter().GetResult();
                WriteObject(tags, true);
            }
        }
    }
}