using System;

namespace PnP.PowerShell.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class RequiredMinimalApiPermissions : Attribute
    {
        public string[] PermissionScopes { get; set; }

        public RequiredMinimalApiPermissions(params string[] permissionScopes)
        {
            PermissionScopes = permissionScopes;
        }
    }
}
