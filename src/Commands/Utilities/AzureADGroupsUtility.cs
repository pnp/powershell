using Group = PnP.PowerShell.Commands.Model.Graph.Group;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class AzureADGroupsUtility
    {
        internal static Group GetGroup(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            var results = GraphHelper.Get<RestResultCollection<Group>>(cmdlet, connection, $"v1.0/groups?$filter=id eq '{groupId}'", accessToken);

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static Group GetGroup(Cmdlet cmdlet, PnPConnection connection, string displayName, string accessToken)
        {
            var results = GraphHelper.Get<RestResultCollection<Group>>(cmdlet, connection, $"v1.0/groups?$filter=(displayName eq '{displayName}' or mailNickName eq '{displayName}')", accessToken);

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static IEnumerable<Group> GetGroups(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var results = GraphHelper.GetResultCollection<Group>(cmdlet, connection, $"v1.0/groups", accessToken, propertyNameCaseInsensitive: true);
            return results;            
        }

        internal static Group Update(Cmdlet cmdlet, PnPConnection connection, string accessToken, Group group)
        {
            return GraphHelper.Patch(cmdlet, connection, accessToken, $"v1.0/groups/{group.Id}", group);
        }

    }
}
