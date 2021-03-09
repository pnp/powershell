using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPAadGroup")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetAadGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public AadGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            GroupEntity group = null;
            List<GroupEntity> groups = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }
            else
            {
                groups = GroupsUtility.GetGroups(AccessToken);
            }

            if (group != null)
            {
                WriteObject(group);
            }
            else if (groups != null)
            {
                WriteObject(groups, true);
            }
        }
    }
}