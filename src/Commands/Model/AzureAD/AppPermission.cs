using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AppPermission
    {
        public string Id { get; set; }

        public string[] Roles { get; set; }

        public List<AppIdentity> Apps { get; set; } = new List<AppIdentity>();
    }
}