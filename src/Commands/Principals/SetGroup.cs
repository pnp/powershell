using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Set, "PnPGroup")]
    [OutputType(typeof(void))]
    
    public class SetGroup : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public GroupPipeBind Identity = new GroupPipeBind();

        [Parameter(Mandatory = false)]
        public AssociatedGroupType SetAssociatedGroup = AssociatedGroupType.None;

        [Parameter(Mandatory = false)]
        public string AddRole = string.Empty;

        [Parameter(Mandatory = false)]
        public string RemoveRole = string.Empty;

        [Parameter(Mandatory = false)]
        public string Title = string.Empty;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public bool AllowRequestToJoinLeave;

        [Parameter(Mandatory = false)]
        public bool AutoAcceptRequestToJoinLeave;

        [Parameter(Mandatory = false)]
        public bool AllowMembersEditMembership;

        [Parameter(Mandatory = false)]
        public bool OnlyAllowMembersViewMembership;

        [Parameter(Mandatory = false)]
        public string RequestToJoinEmail;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(CurrentWeb);

            ClientContext.Load(group,
                g => g.AllowMembersEditMembership,
                g => g.AllowRequestToJoinLeave,
                g => g.AutoAcceptRequestToJoinLeave,
                g => g.OnlyAllowMembersViewMembership,
                g => g.RequestToJoinLeaveEmailSetting);
            ClientContext.ExecuteQueryRetry();

            if (SetAssociatedGroup != AssociatedGroupType.None)
            {
                switch (SetAssociatedGroup)
                {
                    case AssociatedGroupType.Visitors:
                        {
                            CurrentWeb.AssociateDefaultGroups(null, null, group);
                            break;
                        }
                    case AssociatedGroupType.Members:
                        {
                            CurrentWeb.AssociateDefaultGroups(null, group, null);
                            break;
                        }
                    case AssociatedGroupType.Owners:
                        {
                            CurrentWeb.AssociateDefaultGroups(group, null, null);
                            break;
                        }
                }
            }
            if (!string.IsNullOrEmpty(AddRole))
            {
                var roleDefinition = CurrentWeb.RoleDefinitions.GetByName(AddRole);
                var roleDefinitionBindings = new RoleDefinitionBindingCollection(ClientContext);
                roleDefinitionBindings.Add(roleDefinition);
                var roleAssignments = CurrentWeb.RoleAssignments;
                roleAssignments.Add(group, roleDefinitionBindings);
                ClientContext.Load(roleAssignments);
                ClientContext.ExecuteQueryRetry();
            }
            if (!string.IsNullOrEmpty(RemoveRole))
            {
                var roleAssignment = CurrentWeb.RoleAssignments.GetByPrincipal(group);
                var roleDefinitionBindings = roleAssignment.RoleDefinitionBindings;
                ClientContext.Load(roleDefinitionBindings);
                ClientContext.ExecuteQueryRetry();
                foreach (var roleDefinition in roleDefinitionBindings.Where(roleDefinition => roleDefinition.Name == RemoveRole))
                {
                    roleDefinitionBindings.Remove(roleDefinition);
                    roleAssignment.Update();
                    ClientContext.ExecuteQueryRetry();
                    break;
                }
            }

            var dirty = false;
            if (!string.IsNullOrEmpty(Title))
            {
                group.Title = Title;
                dirty = true;
            }
            if (!string.IsNullOrEmpty(Description))
            {
                var groupItem = CurrentWeb.SiteUserInfoList.GetItemById(group.Id);
                CurrentWeb.Context.Load(groupItem, g => g["Notes"]);
                CurrentWeb.Context.ExecuteQueryRetry();

                var groupDescription = groupItem["Notes"]?.ToString();

                if (groupDescription != Description)
                {
                    groupItem["Notes"] = Description;
                    groupItem.Update();
                    dirty = true;
                }

                var plainTextDescription = PnP.Framework.Utilities.PnPHttpUtility.ConvertSimpleHtmlToText(Description, int.MaxValue);
                if (group.Description != plainTextDescription)
                {
                    //If the description is more than 512 characters long a server exception will be thrown.
                    group.Description = plainTextDescription;
                    dirty = true;
                }
            }
            if (ParameterSpecified(nameof(AllowRequestToJoinLeave)) && AllowRequestToJoinLeave != group.AllowRequestToJoinLeave)
            {
                group.AllowRequestToJoinLeave = AllowRequestToJoinLeave;
                dirty = true;
            }

            if (ParameterSpecified(nameof(AutoAcceptRequestToJoinLeave)) && AutoAcceptRequestToJoinLeave != group.AutoAcceptRequestToJoinLeave)
            {
                group.AutoAcceptRequestToJoinLeave = AutoAcceptRequestToJoinLeave;
                dirty = true;
            }
            if (ParameterSpecified(nameof(AllowMembersEditMembership)) && AllowMembersEditMembership != group.AllowMembersEditMembership)
            {
                group.AllowMembersEditMembership = AllowMembersEditMembership;
                dirty = true;
            }
            if (ParameterSpecified(nameof(OnlyAllowMembersViewMembership)) && OnlyAllowMembersViewMembership != group.OnlyAllowMembersViewMembership)
            {
                group.OnlyAllowMembersViewMembership = OnlyAllowMembersViewMembership;
                dirty = true;
            }
            if (RequestToJoinEmail != group.RequestToJoinLeaveEmailSetting)
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
                    groupOwner = CurrentWeb.EnsureUser(Owner);
                    group.Owner = groupOwner;
                    group.Update();
                    ClientContext.ExecuteQueryRetry();
                }
                catch
                {
                    groupOwner = CurrentWeb.SiteGroups.GetByName(Owner);
                    group.Owner = groupOwner;
                    group.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }

        }
    }
}
