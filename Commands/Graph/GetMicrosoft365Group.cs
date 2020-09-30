using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365Group")]
    [Alias("Get-PnPUnifiedGroup")]
   
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_Read_All | MicrosoftGraphApiPermission.Group_ReadWrite_All | MicrosoftGraphApiPermission.GroupMember_ReadWrite_All | MicrosoftGraphApiPermission.GroupMember_Read_All | MicrosoftGraphApiPermission.Directory_ReadWrite_All | MicrosoftGraphApiPermission.Directory_Read_All)]
    public class GetMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter ExcludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeClassification;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeHasTeam;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;
            List<UnifiedGroupEntity> groups = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }
            else
            {
                // Retrieve all the groups
                groups = UnifiedGroupsUtility.GetUnifiedGroups(AccessToken, includeSite: !ExcludeSiteUrl.IsPresent, includeClassification:IncludeClassification.IsPresent, includeHasTeam: IncludeHasTeam.IsPresent);
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