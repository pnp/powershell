namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the possible resource types to generate an access token for
    /// </summary>
    public enum ResourceTypeName
    {
        /// <summary>
        /// Microsoft Graph
        /// </summary>
        Graph = 1,

        /// <summary>
        /// SharePoint Online
        /// </summary>
        SharePoint = 2,

        /// <summary>
        /// Azure Resource Manager
        /// </summary>
        ARM = 3
    }
}