using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class AzureADAppPermission
    {
        public string Id { get; set; }

        public string[] Roles { get; set; }

        public List<AzureADAppIdentity> Apps { get; set; } = new List<AzureADAppIdentity>();

    }

    public class AzureADAppIdentity
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return $"{DisplayName}, {Id}";
        }
    }


}