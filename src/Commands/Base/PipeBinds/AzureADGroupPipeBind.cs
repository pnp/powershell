using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

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

        public Group GetGroup(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            Group group = null;
            if (Group != null)
            {
                group = AzureADGroupsUtility.GetGroup(cmdlet, connection, new Guid(Group.Id), accessToken);
            }
            else if (!string.IsNullOrEmpty(GroupId))
            {
                group = AzureADGroupsUtility.GetGroup(cmdlet, connection, new Guid(GroupId), accessToken);
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = AzureADGroupsUtility.GetGroup(cmdlet, connection, DisplayName, accessToken);
            }
            if (group != null)
            {
                return group;
            }
            return null;
        }
    }
}
