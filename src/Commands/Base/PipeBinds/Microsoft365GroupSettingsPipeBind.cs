using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class Microsoft365GroupSettingsPipeBind
    {
        private readonly Microsoft365GroupSetting _group;
        private readonly Guid _groupId;
        private readonly string _displayName;

        public Microsoft365GroupSettingsPipeBind()
        {
        }

        public Microsoft365GroupSettingsPipeBind(Microsoft365GroupSetting group)
        {
            _group = group;
        }

        public Microsoft365GroupSettingsPipeBind(string input)
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

        public Microsoft365GroupSettingsPipeBind(Guid guid)
        {
            _groupId = guid;
        }

        public Microsoft365GroupSetting Group => _group;

        public String DisplayName => _displayName;

        public Guid GroupId => _groupId;

        public Guid GetGroupSettingId(Cmdlet cmdlet, PnPConnection connection, string accessToken)
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
                var groups = ClearOwners.GetGroupSettings(cmdlet, connection, accessToken);
                if (groups != null)
                {
                    var collection = groups.Where(p => p.displayName.Equals(displayName));
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Group not found");
        }
    }
}
