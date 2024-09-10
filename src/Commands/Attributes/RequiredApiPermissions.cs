using System;

namespace PnP.PowerShell.Commands.Attributes
{
    /// <summary>
    /// Attribute to specify the required permissions for calling into an API. Multiple attributes can be provided and are assumed as ORs towards eachother.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequiredMinimalApiPermissions : Attribute
    {
        /// <summary>
        /// All the permission scopes that are needed to call into the API. Multiple scopes can be provided and are assumed as ANDs towards eachother. 
        /// </summary>
        public string[] PermissionScopes { get; set; }

        /// <summary>
        /// Declares a new set of required permissions for calling into an API. Multiple scopes can be provided and are assumed as ANDs towards eachother.
        /// </summary>
        /// <param name="permissionScopes"></param>
        public RequiredMinimalApiPermissions(params string[] permissionScopes)
        {
            PermissionScopes = permissionScopes;
        }
    }
}
