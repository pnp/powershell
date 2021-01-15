using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupMembers")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetMicrosoft365GroupMembers : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                // Get members of the group
                List<UnifiedGroupUser> members = UnifiedGroupsUtility.GetUnifiedGroupMembers(group, AccessToken);
                WriteObject(members);
            }
        }
    }
}