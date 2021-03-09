using PnP.Framework.Entities;
using PnP.Framework.Graph;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class AadGroupPipeBind
    {
        private readonly GroupEntity _group;
        private readonly String _groupId;
        private readonly String _displayName;

        public AadGroupPipeBind()
        {
        }

        public AadGroupPipeBind(GroupEntity group)
        {
            _group = group;
        }

        public AadGroupPipeBind(String input)
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

        public GroupEntity Group => (_group);

        public String DisplayName => (_displayName);

        public String GroupId => (_groupId);

        public GroupEntity GetGroup(string accessToken)
        {
            GroupEntity group = null;
            if (Group != null)
            {
                group = GroupsUtility.GetGroup(Group.GroupId, accessToken);
            }
            else if (!String.IsNullOrEmpty(GroupId))
            {
                group = GroupsUtility.GetGroup(GroupId, accessToken);
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                var groups = GroupsUtility.GetGroups(accessToken, DisplayName);
                if (groups == null || groups.Count == 0)
                {
                    groups = GroupsUtility.GetGroups(accessToken, mailNickname: DisplayName);
                }
                if (groups != null && groups.Any())
                {
                    group = groups.FirstOrDefault();
                }
            }
            return group;
        }

        public GroupEntity GetDeletedGroup(string accessToken)
        {
            GroupEntity group = null;

            if (Group != null)
            {
                group = GroupsUtility.GetDeletedGroup(Group.GroupId, accessToken);
            }
            else if (!string.IsNullOrEmpty(GroupId))
            {
                group = GroupsUtility.GetDeletedGroup(GroupId, accessToken);
            }

            return group;
        }
    }
}
