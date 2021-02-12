using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPAadGroupOwners")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetAadGroupOwners : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AadGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            GroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                // Get Owners of the group
                List<GroupUser> owners = GroupsUtility.GetGroupOwners(group, AccessToken);
                WriteObject(owners);
            }
        }
    }
}