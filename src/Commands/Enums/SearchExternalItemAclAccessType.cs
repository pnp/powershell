namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the possible ACL access types for external items to define if the ACL instructs a grant or deny
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-acl#properties"/>
    public enum SearchExternalItemAclAccessType : short
    {
        /// <summary>
        /// Grants access
        /// </summary>
        Grant = 0,

        /// <summary>
        /// Denies access
        /// </summary>
        Deny = 1
    }
}
