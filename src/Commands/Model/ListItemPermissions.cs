using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class ListItemPermissionCollection
    {
        public bool HasUniqueRoleAssignments { get; set; }

        public List<Permissions> Permissions { get; set; }
    }

    public class Permissions
    {        
        public List<RoleDefinition> PermissionLevels { get; set; }

        public string PrincipalName { get; set; }

        public PrincipalType PrincipalType { get; set; }

        public int PrincipalId { get; set; }
    }
}
