using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>Builds the permission parameters Register-PnPEntraIDApp exposes, so a cmdlet forwarding to it offers the same ones. The resource list mirrors that cmdlet's and must be kept in step; Register-PnPEntraIDAppForInteractiveLogin holds a third copy already behind at three resources.</summary>
    internal static class EntraIDPermissionParameters
    {
        /// <summary>Resources a permission parameter is offered for; the last three expose delegated permissions only, and would otherwise be reachable through -ResourcePermissions.</summary>
        private static readonly (string Name, string ResourceAppId, bool HasApplicationPermissions)[] resources = new[]
        {
            ("Graph", PermissionScopes.ResourceAppId_Graph, true),
            ("SharePoint", PermissionScopes.ResourceAppId_SPO, true),
            ("O365Management", PermissionScopes.ResourceAppID_O365Management, true),
            ("Exchange", "00000002-0000-0ff1-ce00-000000000000", true),
            ("PowerBI", "00000009-0000-0000-c000-000000000000", true),
            ("Dataverse", "00000007-0000-0000-c000-000000000000", false),
            ("PowerApps", "475226c6-020e-4fb2-8a90-7a972cbfc1d4", false),
            ("AzureServiceManagement", "797f4846-ba00-4fd7-ba43-dac1f8f63013", false)
        };

        /// <summary>The name of every application permission parameter.</summary>
        public static IEnumerable<string> ApplicationParameterNames =>
            resources.Where(resource => resource.HasApplicationPermissions)
                     .Select(resource => $"{resource.Name}ApplicationPermissions");

        /// <summary>The name of every delegated permission parameter.</summary>
        public static IEnumerable<string> DelegateParameterNames =>
            resources.Select(resource => $"{resource.Name}DelegatePermissions");

        /// <summary>The name of every permission parameter, application and delegated.</summary>
        public static IEnumerable<string> AllParameterNames =>
            ApplicationParameterNames.Concat(DelegateParameterNames);

        public static RuntimeDefinedParameterDictionary GetDynamicParameters()
        {
            var parameters = new RuntimeDefinedParameterDictionary();

            foreach (var (name, resourceAppId, hasApplicationPermissions) in resources)
            {
                if (hasApplicationPermissions)
                {
                    parameters.Add($"{name}ApplicationPermissions", GetParameter($"{name}ApplicationPermissions", resourceAppId, "Role"));
                }
                parameters.Add($"{name}DelegatePermissions", GetParameter($"{name}DelegatePermissions", resourceAppId, "Scope"));
            }

            return parameters;
        }

        private static RuntimeDefinedParameter GetParameter(string parameterName, string resourceAppId, string type)
        {
            var attributes = new Collection<Attribute>
            {
                new ParameterAttribute { ValueFromPipeline = false, ValueFromPipelineByPropertyName = false, Mandatory = false }
            };

            // Only module-shipped permissions can be validated up front; the rest are checked against the tenant.
            if (PermissionScopes.IsCuratedResource(resourceAppId))
            {
                attributes.Add(new ValidateSetAttribute(new PermissionScopes().GetIdentifiers(resourceAppId, type)));
            }

            return new RuntimeDefinedParameter(parameterName, typeof(string[]), attributes);
        }
    }
}
