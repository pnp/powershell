using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// The VersionPolicy setting on site
    /// The new document libraries use the setting on site if it is set
    /// Otherwise, it uses the setting on tenant
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
