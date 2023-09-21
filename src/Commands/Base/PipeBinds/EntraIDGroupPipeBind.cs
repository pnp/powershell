using PnP.PowerShell.Commands.Utilities;
using System;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class EntraIDGroupPipeBind
    {
        private readonly Model.EntraID.Group _group;
        private readonly string _groupId;
        private readonly string _displayName;

        public EntraIDGroupPipeBind()
        {
        }

        public EntraIDGroupPipeBind(Model.EntraID.Group group)
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

        public Model.EntraID.Group Group => (_group);

        public string DisplayName => (_displayName);

        public string GroupId => (_groupId);

        public Group GetGroup(PnPConnection connection, string accessToken)
        {
            Group group = null;
            if (Group != null)
            {
                group = EntraIDGroupsUtility.GetGroupAsync(connection, new Guid(Group.Id), accessToken).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(GroupId))
            {
                group = EntraIDGroupsUtility.GetGroupAsync(connection, new Guid(GroupId), accessToken).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = EntraIDGroupsUtility.GetGroupAsync(connection, DisplayName, accessToken).GetAwaiter().GetResult();
            }
            if (group != null)
            {
                return group;
            }
            return null;
        }
    }
}
