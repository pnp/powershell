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
        internal static Group GetGroup(GraphHelper requestHelper, Guid groupId)
        {
            var results = requestHelper.Get<RestResultCollection<Group>>($"v1.0/groups?$filter=id eq '{groupId}'");

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static Group GetGroup(GraphHelper requestHelper, string displayName)
        {
            var results = requestHelper.Get<RestResultCollection<Group>>($"v1.0/groups?$filter=(displayName eq '{displayName}' or mailNickName eq '{displayName}')");

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                return group;
            }
            return null;
        }

        internal static IEnumerable<Group> GetGroups(GraphHelper requestHelper)
        {
            var results = requestHelper.GetResultCollection<Group>($"v1.0/groups", propertyNameCaseInsensitive: true);
            return results;
        }

        internal static Group Update(GraphHelper requestHelper, Group group)
        {
            return requestHelper.Patch($"v1.0/groups/{group.Id}", group);
        }

    }
}
