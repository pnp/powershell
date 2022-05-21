using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Enums;
using System;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.New, "PnPGroup")]
    [OutputType(typeof(Group))]
    public class NewGroup : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title = string.Empty;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowRequestToJoinLeave;

        [Parameter(Mandatory = false)]
        public SwitchParameter AutoAcceptRequestToJoinLeave;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowMembersEditMembership;

        [Parameter(Mandatory = false)]
        [Obsolete("This is done by default. Use DisallowMembersViewMembership to disallow group members viewing membership")]
        public SwitchParameter OnlyAllowMembersViewMembership;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisallowMembersViewMembership;

        [Parameter(Mandatory = false)]
        public string RequestToJoinEmail;

        [Parameter(Mandatory = false)] // Not promoted to use anymore. Use Set-PnPGroup
        [Obsolete("Use Set-PnPGroup.")]
        public AssociatedGroupType SetAssociatedGroup = AssociatedGroupType.None;

        protected override void ExecuteCmdlet()
        {
            var web = CurrentWeb;

            var groupCI = new GroupCreationInformation { Title = Title, Description = Description };

            var group = web.SiteGroups.Add(groupCI);

            ClientContext.Load(group);
            ClientContext.Load(group.Users);
            ClientContext.ExecuteQueryRetry();
            var dirty = false;
            if (AllowRequestToJoinLeave)
            {
                group.AllowRequestToJoinLeave = true;
                dirty = true;
            }

            if (AutoAcceptRequestToJoinLeave)
            {
                group.AutoAcceptRequestToJoinLeave = true;
                dirty = true;
            }
            if (AllowMembersEditMembership)
            {
                group.AllowMembersEditMembership = true;
                dirty = true;
            }
#pragma warning disable 618
            if (OnlyAllowMembersViewMembership)
#pragma warning restore 618
            {
                group.OnlyAllowMembersViewMembership = true;
                dirty = true;
            }
            if (DisallowMembersViewMembership)
            {
                group.OnlyAllowMembersViewMembership = false;
                dirty = true;
            }
            if (!string.IsNullOrEmpty(RequestToJoinEmail))
            {
                group.RequestToJoinLeaveEmailSetting = RequestToJoinEmail;
                dirty = true;
            }

            if (dirty)
            {
                group.Update();
                ClientContext.ExecuteQueryRetry();
            }

            if (!string.IsNullOrEmpty(Owner))
            {
                Principal groupOwner;

                try
                {
                    groupOwner = web.EnsureUser(Owner);
                    group.Owner = groupOwner;
                    group.Update();
                    ClientContext.ExecuteQueryRetry();
                }
                catch
                {
                    groupOwner = web.SiteGroups.GetByName(Owner);
                    group.Owner = groupOwner;
                    group.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }


#pragma warning disable CS0618 // Type or member is obsolete
            if (SetAssociatedGroup != AssociatedGroupType.None)

            {
                switch (SetAssociatedGroup)
                {
                    case AssociatedGroupType.Visitors:
                        {
                            web.AssociateDefaultGroups(null, null, group);
                            break;
                        }
                    case AssociatedGroupType.Members:
                        {
                            web.AssociateDefaultGroups(null, group, null);
                            break;
                        }
                    case AssociatedGroupType.Owners:
                        {
                            web.AssociateDefaultGroups(group, null, null);
                            break;
                        }
                }
            }
#pragma warning restore CS0618 // Type or member is obsolete

            ClientContext.ExecuteQueryRetry();
            WriteObject(group);
        }
    }
}
