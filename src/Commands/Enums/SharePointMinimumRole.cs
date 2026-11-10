using System.ComponentModel;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// The minimum SharePoint role a user needs to hold to be able to run a cmdlet, next to the API permissions that need to be granted.
    /// The values are ordered from least to most privileged, so they can be compared.
    /// </summary>
    public enum SharePointMinimumRole : short
    {
        /// <summary>
        /// The minimum SharePoint role required could not be determined
        /// </summary>
        [Description("Unknown")]
        Unknown = 0,

        /// <summary>
        /// The cmdlet does not act on SharePoint, so no SharePoint role applies
        /// </summary>
        [Description("Not applicable")]
        NotApplicable = 1,

        /// <summary>
        /// Read access on the site the cmdlet acts on, i.e. through the Visitors group
        /// </summary>
        [Description("Read on the target site")]
        SiteVisitor = 2,

        /// <summary>
        /// Contribute access on the site the cmdlet acts on, i.e. through the Members group
        /// </summary>
        [Description("Contribute on the target site")]
        SiteMember = 3,

        /// <summary>
        /// Edit access on the site the cmdlet acts on. Holds the Manage Lists right, needed to manage lists, views, fields and content types, which Contribute does not grant.
        /// </summary>
        [Description("Edit on the target site")]
        SiteEditor = 4,

        /// <summary>
        /// Design access on the site the cmdlet acts on. Holds the Add and Customize Pages, Apply Themes and Borders, Apply Style Sheets and Override List Behaviors rights,
        /// which Edit does not grant.
        /// </summary>
        [Description("Design on the target site")]
        SiteDesigner = 5,

        /// <summary>
        /// Full Control on the site the cmdlet acts on, i.e. through the Owners group. Holds the rights that no other permission level does, such as Manage Permissions,
        /// Enumerate Permissions, Manage Web Site, Create Groups and Manage Alerts.
        /// </summary>
        [Description("Full Control on the target site")]
        SiteOwner = 6,

        /// <summary>
        /// Site collection administrator on the site collection the cmdlet acts on
        /// </summary>
        [Description("Site collection administrator")]
        SiteCollectionAdministrator = 7,

        /// <summary>
        /// SharePoint Administrator or Global Administrator on the tenant
        /// </summary>
        [Description("SharePoint Administrator")]
        SharePointAdministrator = 8
    }
}
