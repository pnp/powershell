using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    /// <summary>
    /// User delta in Entra ID
    /// </summary>
    public class UserDelta
    {
        /// <summary>
        /// User objects with changes or all users if no SkipToken has been provided
        /// </summary>
        public IList<User> Users { get; set; }

        /// <summary>
        /// The DeltaToken which can be used when querying for changes to request changes made to User objects since this DeltaToken has been given out
        /// </summary>
        public string DeltaToken { get; set; }
    }
}
