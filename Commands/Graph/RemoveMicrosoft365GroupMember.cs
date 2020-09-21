using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPMicrosoft365GroupMember")]
    [Alias("Remove-PnPUnifiedGroupMember")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_ReadWrite_All | MicrosoftGraphApiPermission.GroupMember_ReadWrite_All | MicrosoftGraphApiPermission.Directory_ReadWrite_All)]
    public class RemoveMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                UnifiedGroupsUtility.RemoveUnifiedGroupMembers(group.GroupId, Users, AccessToken);
            }
        }
    }
}