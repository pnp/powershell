using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// The progress for the request that settting version policy for existing document libraries of the site
    /// </summary>
    public class SetVersionPolicyProgress
    {
        /// <summary>
        /// Site Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The workitem Id related to the request
        /// </summary>
        public string WorkItemId { get; set; }

        /// <summary>
        /// The request status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The UTC time user sent the request
        /// </summary>
        public string RequestTimeInUTC { get; set; }

        /// <summary>
        /// The UTC time the server last processed the request
        /// </summary>
        public string LastProcessTimeInUTC { get; set; }

        /// <summary>
        /// The UTC time the request completes
        /// </summary>
        public string CompleteTimeInUTC { get; set; }

        /// <summary>
        /// The lists processed count
        /// </summary>
        public string ListsProcessedInTotal { get; set; }

        /// <summary>
        /// The lists failed to process count
        /// </summary>
        public string ListsFailedInTotal { get; set; }

        /// <summary>
        /// Set version policy as AutoExpiration or not
        /// </summary>
        public string EnableAutoTrim { get; set; }

        /// <summary>
        /// The time limit if the version policy is ExpireAfter
        /// </summary>
        public string ExpireAfterDays { get; set; }

        /// <summary>
        /// MajorVersionLimit for the versions
        /// </summary>
        public string MajorVersionLimit { get; set; }

        /// <summary>
        /// MajorWithMinorVersionsLimit for the versions
        /// if minor version is enabled
        /// </summary>
        public string MajorWithMinorVersionsLimit { get; set; }
    }
}
