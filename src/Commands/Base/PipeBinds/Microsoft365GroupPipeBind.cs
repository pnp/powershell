using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class Microsoft365GroupPipeBind
    {
        private readonly Microsoft365Group _group;
        private readonly Guid _groupId;
        private readonly string _displayName;

        public Microsoft365GroupPipeBind()
        {
        }

        public Microsoft365GroupPipeBind(Microsoft365Group group)
        {
            _group = group;
        }

        public Microsoft365GroupPipeBind(string input)
        {
            Guid idValue;
            if (Guid.TryParse(input, out idValue))
            {
                _groupId = idValue;
            }
            else
            {
                _displayName = input;
            }
        }

        public Microsoft365GroupPipeBind(Guid guid)
        {
            _groupId = guid;
        }

        public Microsoft365Group Group => _group;

        public String DisplayName => _displayName;

        public Guid GroupId => _groupId;

        public Microsoft365Group GetGroup(PnPConnection connection, string accessToken, bool includeSite, bool includeOwners, bool detailed)
        {
            Microsoft365Group group = null;
            if (Group != null)
            {
                group = Microsoft365GroupsUtility.GetGroupAsync(connection, _group.Id.Value, accessToken, includeSite, includeOwners, detailed).GetAwaiter().GetResult();
            }
            else if (_groupId != Guid.Empty)
            {
                group = Microsoft365GroupsUtility.GetGroupAsync(connection, _groupId, accessToken, includeSite, includeOwners, detailed).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = Microsoft365GroupsUtility.GetGroupAsync(connection, DisplayName, accessToken, includeSite, includeOwners, detailed).GetAwaiter().GetResult();
            }
            return group;
        }

        public Guid GetGroupId(PnPConnection connection, string accessToken)
        {
            if (Group != null)
            {
                return _group.Id.Value;
            }
            else if (_groupId != Guid.Empty)
            {
                return _groupId;
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                var group = Microsoft365GroupsUtility.GetGroupAsync(connection, DisplayName, accessToken, false, false, false).GetAwaiter().GetResult();
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Group not found");
        }

        public Microsoft365Group GetDeletedGroup(PnPConnection connection, string accessToken)
        {
            if (_group != null)
            {
                return Microsoft365GroupsUtility.GetDeletedGroupAsync(connection, _group.Id.Value, accessToken).GetAwaiter().GetResult();
            }
            else if (_groupId != Guid.Empty)
            {
                return Microsoft365GroupsUtility.GetDeletedGroupAsync(connection, _groupId, accessToken).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(_displayName))
            {
                return Microsoft365GroupsUtility.GetDeletedGroupAsync(connection, _displayName, accessToken).GetAwaiter().GetResult();
            }
            return null;
        }

        public Guid GetDeletedGroupId(PnPConnection connection, string accessToken)
        {
            if (_group != null)
            {
                return _group.Id.Value;
            }
            else if (_groupId != Guid.Empty)
            {
                return _groupId;
            }
            else if (!string.IsNullOrEmpty(_displayName))
            {
                var group = Microsoft365GroupsUtility.GetDeletedGroupAsync(connection, _displayName, accessToken).GetAwaiter().GetResult();
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Deleted group not found");
        }
    }
}
