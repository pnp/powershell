using System;
using System.Collections.Generic;
using System.Text;

namespace PnP.PowerShell.Commands.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class PnPManagementShellScopesAttribute : Attribute
    {
        public string[] PermissionScopes { get; set; }

        public PnPManagementShellScopesAttribute(params string[] permissionScopes)
        {
            PermissionScopes = permissionScopes;
        }
    }
}
