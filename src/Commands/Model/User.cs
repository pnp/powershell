namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Contains information about a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier of the user in the user information list of the site collection
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Display name of the user (a.k.a. Title)
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The login name of the user
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; }
    }
}