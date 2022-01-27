using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsPrimaryChannel")]
    [RequiredMinimalApiPermissions("Channel.ReadBasic.All")]

    public class GetTeamsPrimaryChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            { 
              WriteObject(TeamsUtility.GetPrimaryChannelAsync(AccessToken, HttpClient, groupId).GetAwaiter().GetResult());
            } else
            {
                throw new PSArgumentException("Team not found", nameof(Team));
            }
        }
    }
}