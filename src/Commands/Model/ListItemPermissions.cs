using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class ListItemPermissions
    {
        public IEnumerable<Core.Model.Security.IRoleDefinition> RoleDefinitions { get; set; }

        public int PrincipalId { get; set; }
    }
}
