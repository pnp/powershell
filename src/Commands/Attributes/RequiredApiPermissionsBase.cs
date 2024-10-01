using System;
using System.Linq;
using System.Text.RegularExpressions;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Base class for attributes to specify the required permissions for calling into an API. Multiple attributes can be provided and are assumed as ORs towards eachother.
    /// </summary>
    public abstract class RequiredApiPermissionsBase : Attribute
    {
        /// <summary>
        /// All the permission scopes that are needed to call into the API. Multiple scopes can be provided and are assumed as ANDs towards eachother. 
        /// </summary>
        public RequiredApiPermission[] PermissionScopes { get; set; }

        /// <summary>
        /// Declares one required permission for calling into an API
        /// </summary>
        /// <param name="resourceType">Type of resource that access is needed to</param>
        /// <param name="scope">Scope on the resource that access is needed to</param>
        public RequiredApiPermissionsBase(Enums.ResourceTypeName resourceType, string scope)
        {
            PermissionScopes = new[] { new RequiredApiPermission(resourceType, scope) };
        }

        /// <summary>
        /// Declares a new set of required permissions for calling into an API. Multiple scopes can be provided and are assumed as ANDs towards eachother.
        /// </summary>
        /// <param name="permissionScopes">One or more permission scopes in the format https://<resource>/<scope>, i.e. https://graph.microsoft.com/Group.Read.All</param>
        public RequiredApiPermissionsBase(params string[] permissionScopes)
        {
            // Try to transform each of the permission scopes to a types model equivallent
            PermissionScopes = permissionScopes.Select(ps => {
                // Use a regular expression to pull apart the resource and scope from the permission scope
                var permissionScopeMatch = Regex.Match(ps, "(?:https://)?(?<resource>[^/]*?)/(?<scope>.*)", RegexOptions.IgnoreCase);

                if (permissionScopeMatch.Success && permissionScopeMatch.Groups["resource"].Success && permissionScopeMatch.Groups["scope"].Success)
                {
                    // Match found, match with enum equivallent
                    var resource = TokenHandler.DefineResourceTypeFromAudience(permissionScopeMatch.Groups["resource"].Value);
                    if (resource != Enums.ResourceTypeName.Unknown)
                    {
                        return new RequiredApiPermission(resource, permissionScopeMatch.Groups["scope"].Value);
                    }
                }

                // Unable to parse permission scope, will be ignored
                return null;
            }).ToArray();
        }
    }
}
