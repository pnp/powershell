namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the possible states of an external connection
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-externalconnection?view=graph-rest-1.0#properties"/>
    public enum SearchExternalConnectionState : short
    {
        /// <summary>
        /// Connection is in draft state
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Connection is ready to be used
        /// </summary>
        Ready = 1,

        /// <summary>
        /// Connection can no longer be used
        /// </summary>
        Obsolete = 2,

        /// <summary>
        /// Too many connections have been created on the tenant
        /// </summary>
        LimitExceeded = 3
    }
}
