using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to specify the required application permissions for calling into an API. Multiple attributes can be provided and are assumed as ORs towards eachother.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequiredApiApplicationPermissions : RequiredApiPermissionsBase
    {
        /// <summary>
        /// Declares one required permission for calling into an API
        /// </summary>
        /// <param name="resourceType">Type of resource that access is needed to</param>
        /// <param name="scope">Scope on the resource that access is needed to</param>
        public RequiredApiApplicationPermissions(Enums.ResourceTypeName resourceType, string scope) : base(resourceType, scope)
        {            
        }

        /// <summary>
        /// Declares a new set of required permissions for calling into an API. Multiple scopes can be provided and are assumed as ANDs towards eachother.
        /// </summary>
        /// <param name="permissionScopes">One or more permission scopes in the format https://<resource>/<scope>, i.e. https://graph.microsoft.com/Group.Read.All</param>
        public RequiredApiApplicationPermissions(params string[] permissionScopes) : base(permissionScopes)
        {
        }
    }
}
