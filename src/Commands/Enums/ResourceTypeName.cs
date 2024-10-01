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
        AzureManagementApi = 3,

        /// <summary>
        /// Exchange Online
        /// </summary>
        [Description("Exchange Online")]
        ExchangeOnline = 4,

        /// <summary>
        /// Power Automate
        /// </summary>
        [Description("Power Automate")]
        PowerAutomate = 5,

        /// <summary>
        /// Power Apps
        /// </summary>
        [Description("Power Apps")]
        PowerApps = 6,

        /// <summary>
        /// Dynamics CRM
        /// </summary>
        [Description("Dynamics CRM")]
        DynamicsCRM = 7
    }
}