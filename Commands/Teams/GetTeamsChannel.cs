using PnP.Framework.Graph;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannel")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_Read_All)]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    public class GetTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false)]
        public TeamsChannelPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    WriteObject(Identity.GetChannel(HttpClient, AccessToken, groupId));
                }
                else
                {
                    WriteObject(TeamsUtility.GetChannelsAsync(AccessToken, HttpClient, groupId).GetAwaiter().GetResult());
                }
            } else
            {
                throw new PSArgumentException("Team not found", nameof(Team));
            }
        }
    }
}