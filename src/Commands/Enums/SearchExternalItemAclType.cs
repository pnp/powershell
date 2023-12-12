namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the possible types of ACLs that can be set on external items to define to what type of object the access will be provided
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-acl#properties"/>
    public enum SearchExternalItemAclType : short
    {
        /// <summary>
        /// Access will be provided to a user
        /// </summary>
        User = 0,

        /// <summary>
        /// Access will be provided to a group
        /// </summary>
        Group = 1,

        /// <summary>
        /// Access will be provided to everyone
        /// </summary>
        Everyone = 2,

        /// <summary>
        /// Access will be provided to everyone except external users
        /// </summary>
        EveryoneExceptExternalUsers = 3,

        /// <summary>
        /// Access will be provided to an external group
        /// </summary>
        ExternalGroup = 4
    }
}
