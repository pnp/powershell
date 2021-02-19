using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Clear, "PnPMicrosoft365GroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class ClearMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken, false);
            }

            if (group != null)
            {
                UnifiedGroupsUtility.ClearUnifiedGroupMembers(group.GroupId, AccessToken, azureEnvironment: PnPConnection.Current.AzureEnvironment);
            }
        }
    }
}