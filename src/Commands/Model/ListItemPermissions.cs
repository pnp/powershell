using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class ListItemPermissions
    {
        public List<string> Permissions { get; set; }

        public string PrincipalName { get; set; }
    }
}
