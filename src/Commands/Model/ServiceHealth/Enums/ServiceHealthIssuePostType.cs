namespace PnP.PowerShell.Commands.Model.ServiceHealth.Enums
{
    /// <summary>
    /// The type of post done for a service health issue
    /// </summary>
    public enum ServiceHealthIssuePostType
    {
        /// <summary>
        /// Quick update
        /// </summary>
        Quick,
        /// <summary>
        /// Regular update
        /// </summary>
        Regular,
        /// <summary>
        /// Strategic update
        /// </summary>
        Strategic,
        /// <summary>
        /// Unknown post type
        /// </summary>
        UnknownFutureValue
    }
}