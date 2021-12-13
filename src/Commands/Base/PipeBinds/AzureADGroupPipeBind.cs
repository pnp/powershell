using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Model.AzureAD;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class AzureADGroupPipeBind
    {
        private readonly AzureADGroup _group;
        private readonly string _groupId;
        private readonly string _displayName;

        public AzureADGroupPipeBind()
        {
        }

        public AzureADGroupPipeBind(AzureADGroup group)
        {
            _group = group;
        }

        public AzureADGroupPipeBind(string input)
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

        public AzureADGroup Group => (_group);

        public string DisplayName => (_displayName);

        public string GroupId => (_groupId);

        public AzureADGroup GetGroup(string accessToken)
        {
            GroupEntity group = null;
            if (Group != null)
            {
                group = GroupsUtility.GetGroup(Group.Id, accessToken);
            }
            else if (!string.IsNullOrEmpty(GroupId))
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
            if (group != null)
            {
                return AzureADGroup.CreateFrom(group);
            }
            return null;
        }

        public AzureADGroup GetDeletedGroup(string accessToken)
        {
            GroupEntity group = null;

            if (Group != null)
            {
                group = GroupsUtility.GetDeletedGroup(Group.Id, accessToken);
            }
            else if (!string.IsNullOrEmpty(GroupId))
            {
                group = GroupsUtility.GetDeletedGroup(GroupId, accessToken);
            }

            return AzureADGroup.CreateFrom(group);
        }
    }
}
