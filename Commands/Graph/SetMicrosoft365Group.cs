using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Set, "PnPMicrosoft365Group")]
    [Alias("Set-PnPUnifiedGroup")]
    [MicrosoftGraphApiPermissionCheckAttribute(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]

    public class SetMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public String[] Owners;

        [Parameter(Mandatory = false)]
        public String[] Members;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsPrivate;

        [Parameter(Mandatory = false)]
        public string GroupLogoPath;

        [Parameter(Mandatory = false)]
        public SwitchParameter CreateTeam;

        [Parameter(Mandatory = false)]
        public bool? HideFromAddressLists;

        [Parameter(Mandatory = false)]
        public bool? HideFromOutlookClients;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            Stream groupLogoStream = null;

            if (group != null)
            {
                if (GroupLogoPath != null)
                {
                    if (!Path.IsPathRooted(GroupLogoPath))
                    {
                        GroupLogoPath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, GroupLogoPath);
                    }
                    groupLogoStream = new FileStream(GroupLogoPath, FileMode.Open, FileAccess.Read);
                }
                bool? isPrivateGroup = null;
                if (IsPrivate.IsPresent)
                {
                    isPrivateGroup = IsPrivate.ToBool();
                }
                try
                {
                    UnifiedGroupsUtility.UpdateUnifiedGroup(
                        groupId: group.GroupId,
                        accessToken: AccessToken,
                        displayName: DisplayName,
                        description: Description,
                        owners: Owners,
                        members: Members,
                        groupLogo: groupLogoStream,
                        isPrivate: isPrivateGroup,
                        createTeam: CreateTeam);

                    if (ParameterSpecified(nameof(HideFromAddressLists)) || ParameterSpecified(nameof(HideFromOutlookClients)))
                    {
                        // For this scenario a separate call needs to be made
                        UnifiedGroupsUtility.SetUnifiedGroupVisibility(group.GroupId, AccessToken, HideFromAddressLists, HideFromOutlookClients);
                    }
                }
                catch(Exception e)
                {
                    while (e.InnerException != null) e = e.InnerException;
                    WriteError(new ErrorRecord(e, "GROUPUPDATEFAILED", ErrorCategory.InvalidOperation, this));
                }
            }
            else
            {
                WriteError(new ErrorRecord(new Exception("Group not found"), "GROUPNOTFOUND", ErrorCategory.ObjectNotFound, this));
            }
        }
    }
}