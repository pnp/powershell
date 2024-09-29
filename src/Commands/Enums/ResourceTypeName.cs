using System.ComponentModel;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the possible resource types to generate an access token for
    /// </summary>
    public enum ResourceTypeName
    {
        /// <summary>
        /// Unknown resource type
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Microsoft Graph
        /// </summary>
        [Description("Microsoft Graph")]
        Graph = 1,

        /// <summary>
        /// SharePoint Online
        /// </summary>
        [Description("SharePoint Online")]
        SharePoint = 2,

        /// <summary>
        /// Azure Resource Manager
        /// </summary>
        [Description("Azure ARM")]
        AzureManagementApi = 3
    }
}