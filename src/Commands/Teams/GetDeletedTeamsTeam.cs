﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPDeletedTeam")]
    [RequiredMinimalApiPermissions("Team.ReadBasic.All")]
    [OutputType(typeof(Model.Teams.DeletedTeam))]
    public class GetDeletedTeamsTeam : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(TeamsUtility.GetDeletedTeamAsync(this, AccessToken, Connection).GetAwaiter().GetResult());
        }
    }
}
