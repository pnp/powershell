using System;

namespace PnP.PowerShell.Commands.Model.Graph
{
    public class Identity : IEquatable<Identity>
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UserPrincipalName { get; set; }
        public string UserType { get; set; }

        public bool Equals(Identity other)
        {
            return this.UserPrincipalName.Equals(other.UserPrincipalName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
