using System.ComponentModel;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// The minimum SharePoint role or permission level a user needs to hold to be able to run a cmdlet, next to the API permissions that need to be granted
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
        /// Edit access on the site the cmdlet acts on. Needed to manage lists, views, fields and content types, which the Contribute permission level does not allow.
        /// </summary>
        [Description("Edit on the target site")]
        SiteEditor = 7,

        /// <summary>
        /// Full Control on the site the cmdlet acts on, i.e. through the Owners group
        /// </summary>
        [Description("Full Control on the target site")]
        SiteOwner = 4,

        /// <summary>
        /// Site collection administrator on the site collection the cmdlet acts on
        /// </summary>
        [Description("Site collection administrator")]
        SiteCollectionAdministrator = 5,

        /// <summary>
        /// SharePoint Administrator or Global Administrator on the tenant
        /// </summary>
        [Description("SharePoint Administrator")]
        SharePointAdministrator = 6
    }
}
