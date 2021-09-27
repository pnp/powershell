namespace PnP.PowerShell.Commands.Model.AzureAD
{
    /// <summary>
    /// User in Azure Active Directory
    /// </summary>
    public class User
    {
        /// <summary>
        /// The User Principal Name of the user
        /// </summary>
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// The friendly display name of the user
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The GUID of the user object
        /// </summary>
        public System.Guid? Id { get; set; }

        /// <summary>
        /// Converts a PnP Framework User to a PnP PowerShell Azure Active Directory User
        /// </summary>
        /// <param name="entity">PnP Framework user object</param>
        /// <returns>PnP PowerShell Azure Active Directory User object</returns>
        internal static User CreateFrom(PnP.Framework.Graph.Model.User entity)
        {
            var user = new User
            {
                UserPrincipalName = entity.UserPrincipalName,
                DisplayName = entity.DisplayName
            };
            return user;
        }
    }
}