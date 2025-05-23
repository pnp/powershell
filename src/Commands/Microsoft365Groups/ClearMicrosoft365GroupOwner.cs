﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Clear, "PnPMicrosoft365GroupOwner")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class ClearMicrosoft365GroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Identity.GetGroupId(GraphRequestHelper);
            Microsoft365GroupsUtility.ClearOwnersAsync(GraphRequestHelper, groupId);
            var owners = Microsoft365GroupsUtility.GetOwners(GraphRequestHelper, groupId);
            if (owners != null && owners.Any())
            {
                LogWarning($"Clearing all owners is not possible as there will always have to be at least one owner. To changed the owners with new owners use Set-PnPMicrosoft365GroupOwner -Identity {groupId} -Owners \"newowner@domain.com\"");
                LogWarning($"Current owner is: {owners.First().UserPrincipalName}");
            }
        }
    }
}