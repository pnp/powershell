using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAadGroup")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemoveAadGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AadGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                GroupEntity group = Identity.GetGroup(AccessToken);
                
                if (group != null)
                {
                    GroupsUtility.DeleteGroup(group.GroupId, AccessToken);
                }
            }
        }
    }
}