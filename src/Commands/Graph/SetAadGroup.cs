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
    [Cmdlet(VerbsCommon.Set, "PnPAadGroup")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class SetAadGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AadGroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public String[] Owners;

        [Parameter(Mandatory = false)]
        public String[] Members;

        [Parameter(Mandatory = false)]
        public bool? SecurityEnabled;

        [Parameter(Mandatory = false)]
        public bool? MailEnabled;

        [Parameter(Mandatory = false)]
        public bool? HideFromAddressLists;

        [Parameter(Mandatory = false)]
        public bool? HideFromOutlookClients;

        protected override void ExecuteCmdlet()
        {
            GroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                try
                {
                    GroupsUtility.UpdateGroup(
                        groupId: group.GroupId,
                        accessToken: AccessToken,
                        displayName: DisplayName,
                        description: Description,
                        owners: Owners,
                        members: Members,
                        mailEnabled: group.MailEnabled,
                        securityEnabled: group.SecurityEnabled
                        );

                    if (ParameterSpecified(nameof(HideFromAddressLists)) || ParameterSpecified(nameof(HideFromOutlookClients)))
                    {
                        // For this scenario a separate call needs to be made
                        GroupsUtility.SetGroupVisibility(group.GroupId, AccessToken, HideFromAddressLists, HideFromOutlookClients);
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