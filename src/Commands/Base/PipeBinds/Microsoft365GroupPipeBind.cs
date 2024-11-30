using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
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

        public Microsoft365Group GetGroup(GraphHelper requestHelper, bool includeSite, bool includeOwners, bool detailed, bool includeSensitivityLabels)
        {
            Microsoft365Group group = null;
            if (Group != null)
            {
                group = ClearOwners.GetGroup(requestHelper, _group.Id.Value, includeSite, includeOwners, detailed, includeSensitivityLabels);
            }
            else if (_groupId != Guid.Empty)
            {
                group = ClearOwners.GetGroup(requestHelper, _groupId, includeSite, includeOwners, detailed, includeSensitivityLabels);
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = ClearOwners.GetGroup(requestHelper, DisplayName, includeSite, includeOwners, detailed, includeSensitivityLabels);
            }
            return group;
        }

        public Guid GetGroupId(GraphHelper requestHelper)
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
                var group = ClearOwners.GetGroup(requestHelper, DisplayName, false, false, false, false);
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Group not found");
        }

        public Microsoft365Group GetDeletedGroup(GraphHelper requestHelper)
        {
            if (_group != null)
            {
                return ClearOwners.GetDeletedGroup(requestHelper, _group.Id.Value);
            }
            else if (_groupId != Guid.Empty)
            {
                return ClearOwners.GetDeletedGroup(requestHelper, _groupId);
            }
            else if (!string.IsNullOrEmpty(_displayName))
            {
                return ClearOwners.GetDeletedGroup(requestHelper, _displayName);
            }
            return null;
        }

        public Guid GetDeletedGroupId(GraphHelper requestHelper)
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
                var group = ClearOwners.GetDeletedGroup(requestHelper, _displayName);
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Deleted group not found");
        }
    }
}
