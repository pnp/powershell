﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;
using System.Linq;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Clear, "PnPAzureADGroupOwner")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Clear-PnPEntraIDGroupOwner")]
    public class ClearAzureADGroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(RequestHelper);
            }

            if (group != null)
            {
                var owners = ClearOwners.GetOwners(RequestHelper, new System.Guid(group.Id));

                var ownersToBeRemoved = owners?.Select(p => p.UserPrincipalName).ToArray();

                ClearOwners.RemoveOwners(RequestHelper, new System.Guid(group.Id), ownersToBeRemoved);
            }
        }
    }
}