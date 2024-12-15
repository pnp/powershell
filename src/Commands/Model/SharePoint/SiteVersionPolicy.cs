namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// The VersionPolicy setting on site
    /// When the new document libraries are created, they will be set as the version policy of the site.
    /// If the version policy is not set on the site, the setting on the tenant will be used.
    /// </summary>
    public class SiteVersionPolicy
    {
        /// <summary>
        /// Site Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Site DefaultTrimMode
        /// e.g. AutoExpiration, ExpireAfter, NoExpiration
        /// </summary>
        public string DefaultTrimMode { get; set; }

        /// <summary>
        /// Site DefaultExpireAfterDays
        /// </summary>
        public string DefaultExpireAfterDays { get; set; }

        /// <summary>
        /// Site MajorVersionLimit
        /// </summary>
        public string MajorVersionLimit { get; set; }

        /// <summary>
        /// Results description
        /// </summary>
        public string Description { get; set; }
    }
}
