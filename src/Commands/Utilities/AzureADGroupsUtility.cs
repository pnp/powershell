using Group = PnP.PowerShell.Commands.Model.Graph.Group;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class AzureADGroupsUtility
    {
        internal static async Task<Group> GetGroupAsync(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            var results = await GraphHelper.GetAsync<RestResultCollection<Group>>(cmdlet, connection, $"v1.0/groups?$filter=id eq '{groupId}'", accessToken);

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static async Task<Group> GetGroupAsync(Cmdlet cmdlet, PnPConnection connection, string displayName, string accessToken)
        {
            var results = await GraphHelper.GetAsync<RestResultCollection<Group>>(cmdlet, connection, $"v1.0/groups?$filter=(displayName eq '{displayName}' or mailNickName eq '{displayName}')", accessToken);

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static async Task<IEnumerable<Group>> GetGroupsAsync(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var results = await GraphHelper.GetResultCollectionAsync<Group>(cmdlet, connection, $"v1.0/groups", accessToken, propertyNameCaseInsensitive: true);
            return results;            
        }

        internal static async Task<Group> UpdateAsync(Cmdlet cmdlet, PnPConnection connection, string accessToken, Group group)
        {
            return await GraphHelper.PatchAsync(cmdlet, connection, accessToken, $"v1.0/groups/{group.Id}", group);
        }

    }
}
