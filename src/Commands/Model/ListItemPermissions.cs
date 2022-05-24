using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class ListItemPermissions
    {
        public List<RoleDefinition> Permissions { get; set; }

        public string Principal { get; set; }

        public PrincipalType PrincipalType { get; set; }

        public int PrincipalId { get; set; }
    }
}
