using Group = PnP.PowerShell.Commands.Model.Graph.Group;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Utilities.EntraID
{
    /// <summary>
    /// Contains functionality towards groups in Entra ID
    /// </summary>
    internal static class GroupsUtility
    {
        internal static async Task<Group> GetGroupAsync(PnPConnection connection, Guid groupId, string accessToken)
        {
            var results = await GraphHelper.GetAsync<RestResultCollection<Group>>(connection, $"v1.0/groups?$filter=id eq '{groupId}'", accessToken);

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static async Task<Group> GetGroupAsync(PnPConnection connection, string displayName, string accessToken)
        {
            var results = await GraphHelper.GetAsync<RestResultCollection<Group>>(connection, $"v1.0/groups?$filter=(displayName eq '{displayName}' or mailNickName eq '{displayName}')", accessToken);

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static async Task<IEnumerable<Group>> GetGroupsAsync(PnPConnection connection, string accessToken)
        {
            var results = await GraphHelper.GetResultCollectionAsync<Group>(connection, $"v1.0/groups", accessToken, propertyNameCaseInsensitive: true);
            return results;
        }

        internal static async Task<Group> UpdateAsync(PnPConnection connection, string accessToken, Group group)
        {
            return await GraphHelper.PatchAsync(connection, accessToken, $"v1.0/groups/{group.Id}", group);
        }
    }
}
