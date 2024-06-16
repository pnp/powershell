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

        public Microsoft365Group GetGroup(Cmdlet cmdlet, PnPConnection connection, string accessToken, bool includeSite, bool includeOwners, bool detailed, bool includeSensitivityLabels)
        {
            Microsoft365Group group = null;
            if (Group != null)
            {
                group = ClearOwners.GetGroup(cmdlet, connection, _group.Id.Value, accessToken, includeSite, includeOwners, detailed, includeSensitivityLabels);
            }
            else if (_groupId != Guid.Empty)
            {
                group = ClearOwners.GetGroup(cmdlet, connection, _groupId, accessToken, includeSite, includeOwners, detailed, includeSensitivityLabels);
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = ClearOwners.GetGroup(cmdlet, connection, DisplayName, accessToken, includeSite, includeOwners, detailed, includeSensitivityLabels);
            }
            return group;
        }

        public Guid GetGroupId(Cmdlet cmdlet, PnPConnection connection, string accessToken)
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
                var group = ClearOwners.GetGroup(cmdlet, connection, DisplayName, accessToken, false, false, false, false);
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Group not found");
        }

        public Microsoft365Group GetDeletedGroup(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            if (_group != null)
            {
                return ClearOwners.GetDeletedGroup(cmdlet, connection, _group.Id.Value, accessToken);
            }
            else if (_groupId != Guid.Empty)
            {
                return ClearOwners.GetDeletedGroup(cmdlet, connection, _groupId, accessToken);
            }
            else if (!string.IsNullOrEmpty(_displayName))
            {
                return ClearOwners.GetDeletedGroup(cmdlet, connection, _displayName, accessToken);
            }
            return null;
        }

        public Guid GetDeletedGroupId(Cmdlet cmdlet, PnPConnection connection, string accessToken)
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
                var group = ClearOwners.GetDeletedGroup(cmdlet, connection, _displayName, accessToken);
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Deleted group not found");
        }
    }
}
