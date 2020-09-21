using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupOwners")]
    [Alias("Get-PnPUnifiedGroupOwners")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_Read_All | MicrosoftGraphApiPermission.User_Read_All | MicrosoftGraphApiPermission.Group_ReadWrite_All | MicrosoftGraphApiPermission.User_ReadWrite_All)]
    public class GetMicrosoft365GroupOwners : PnPGraphCmdlet
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
                // Get Owners of the group
                List<UnifiedGroupUser> owners = UnifiedGroupsUtility.GetUnifiedGroupOwners(group, AccessToken);
                WriteObject(owners);
            }
        }
    }
}