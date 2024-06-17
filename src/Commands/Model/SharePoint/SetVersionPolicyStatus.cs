using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// The progress for the request that settting version policy for existing document libraries of the site
    /// </summary>
    public class SetVersionPolicyStatus
    {
        /// <summary>
        /// Site Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The workitem Id related to the request
        /// </summary>
        [JsonPropertyName("WorkItemId")]
        public string WorkItemId { get; set; }

        /// <summary>
        /// The request status
        /// </summary>
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        /// <summary>
        /// The UTC time user sent the request
        /// </summary>
        [JsonPropertyName("RequestTimeInUTC")]
        public string RequestTimeInUTC { get; set; }

        /// <summary>
        /// The UTC time the server last processed the request
        /// </summary>
        [JsonPropertyName("LastProcessTimeInUTC")]
        public string LastProcessTimeInUTC { get; set; }

        /// <summary>
        /// The UTC time the request completes
        /// </summary>
        [JsonPropertyName("CompleteTimeInUTC")]
        public string CompleteTimeInUTC { get; set; }

        /// <summary>
        /// The lists processed count
        /// </summary>
        [JsonPropertyName("ListsProcessedInTotal")]
        public string LibrariesProcessedInTotal { get; set; }

        /// <summary>
        /// The lists failed to process count
        /// </summary>
        [JsonPropertyName("ListsFailedInTotal")]
        public string LibrariesFailedInTotal { get; set; }

        /// <summary>
        /// Set version policy as AutoExpiration or not
        /// </summary>
        [JsonPropertyName("EnableAutoTrim")]
        public string EnableAutomaticMode{ get; set; }

        /// <summary>
        /// The time limit if the version policy is ExpireAfter
        /// </summary>
        [JsonPropertyName("ExpireAfterDays")]
        public string ExpireAfterDays { get; set; }

        /// <summary>
        /// MajorVersionLimit for the versions
        /// </summary>
        [JsonPropertyName("MajorVersionLimit")]
        public string MajorVersionLimit { get; set; }

        /// <summary>
        /// MajorWithMinorVersionsLimit for the versions
        /// if minor version is enabled
        /// </summary>
        [JsonPropertyName("MajorWithMinorVersionsLimit")]
        public string MajorWithMinorVersionsLimit { get; set; }
    }
}
