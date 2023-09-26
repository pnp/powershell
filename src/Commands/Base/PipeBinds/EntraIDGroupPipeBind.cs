using PnP.PowerShell.Commands.Utilities.EntraID;
using System;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class EntraIDGroupPipeBind
    {
        private readonly Model.AzureAD.Group _group;
        private readonly string _groupId;
        private readonly string _displayName;

        public EntraIDGroupPipeBind()
        {
        }

        public EntraIDGroupPipeBind(Model.AzureAD.Group group)
        {
            _group = group;
        }

        public EntraIDGroupPipeBind(string input)
        {
            Guid idValue;
            if (Guid.TryParse(input, out idValue))
            {
                _groupId = input;
            }
            else
            {
                _displayName = input;
            }
        }

        public Model.AzureAD.Group Group => (_group);

        public string DisplayName => (_displayName);

        public string GroupId => (_groupId);

        public Group GetGroup(PnPConnection connection, string accessToken)
        {
            Group group = null;
            if (Group != null)
            {
                group = GroupsUtility.GetGroupAsync(connection, new Guid(Group.Id), accessToken).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(GroupId))
            {
                group = GroupsUtility.GetGroupAsync(connection, new Guid(GroupId), accessToken).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = GroupsUtility.GetGroupAsync(connection, DisplayName, accessToken).GetAwaiter().GetResult();
            }
            if (group != null)
            {
                return group;
            }
            return null;
        }
    }
}
