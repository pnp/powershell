using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPMicrosoft365GroupOwner")]
    [Alias("Add-PnPUnifiedGroupOwner")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.None, MicrosoftGraphApiPermission.User_ReadWrite_All | MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    public class AddMicrosoft365GroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveExisting;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                UnifiedGroupsUtility.AddUnifiedGroupOwners(group.GroupId, Users, AccessToken, RemoveExisting.ToBool());
            }
        }
    }
}