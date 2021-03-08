using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;

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

        public Microsoft365Group GetGroup(HttpClient httpClient, string accessToken, bool includeSite, bool includeOwners)
        {
            Microsoft365Group group = null;
            if (Group != null)
            {
                group = Microsoft365GroupsUtility.GetGroupAsync(httpClient, _group.Id.Value, accessToken, includeSite, includeOwners).GetAwaiter().GetResult();
            }
            else if (_groupId != Guid.Empty)
            {
                group = Microsoft365GroupsUtility.GetGroupAsync(httpClient, _groupId, accessToken, includeSite, includeOwners).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(DisplayName))
            {
                group = Microsoft365GroupsUtility.GetGroupAsync(httpClient, DisplayName, accessToken, includeSite, includeOwners).GetAwaiter().GetResult();
            }
            return group;
        }

        public Guid GetGroupId(HttpClient httpClient, string accessToken)
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
                var group = Microsoft365GroupsUtility.GetGroupAsync(httpClient, DisplayName, accessToken, false, false).GetAwaiter().GetResult();
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Group not found");
            //return Guid.Empty;
        }

        public Microsoft365Group GetDeletedGroup(HttpClient httpClient, string accessToken)
        {
            if (_group != null)
            {
                return Microsoft365GroupsUtility.GetDeletedGroupAsync(httpClient, _group.Id.Value, accessToken).GetAwaiter().GetResult();
            }
            else if (_groupId != Guid.Empty)
            {
                return Microsoft365GroupsUtility.GetDeletedGroupAsync(httpClient, _groupId, accessToken).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(_displayName))
            {
                return Microsoft365GroupsUtility.GetDeletedGroupAsync(httpClient, _displayName, accessToken).GetAwaiter().GetResult();
            }
            return null;
        }

        public Guid GetDeletedGroupId(HttpClient httpClient, string accessToken)
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
                var group = Microsoft365GroupsUtility.GetDeletedGroupAsync(httpClient, _displayName, accessToken).GetAwaiter().GetResult();
                if (group != null)
                {
                    return group.Id.Value;
                }
            }
            throw new PSInvalidOperationException("Deleted group not found");
        }
    }
}
