using System;

namespace PnP.PowerShell.Commands.Model.Syntex
{
    /// <summary>
    /// Result of a classify and extract operation requested for a file
    /// </summary>
    public class SyntexClassifyAndExtractResult
    {
        /// <summary>
        /// Date of the classify and extract request creation
        /// </summary>
        public DateTime Created { get; internal set; }

        /// <summary>
        /// Date of the classify and extract request delivery
        /// </summary>
        public DateTime DeliverDate { get; internal set; }

        /// <summary>
        /// Id of the classify and extract request
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Type of this Syntex machine learning work item
        /// </summary>
        public Guid WorkItemType { get; internal set; }

        /// <summary>
        /// The classify and extract error (if there was any)
        /// </summary>
        public string ErrorMessage { get; internal set; }

        /// <summary>
        /// The status code of the classify and extract request, 2xx == success
        /// </summary>
        public int StatusCode { get; internal set; }

        /// <summary>
        /// Status of the classify and extract request
        /// </summary>
        public string Status { get; internal set; }

        /// <summary>
        /// Server relative url of the file requested to be classified and extracted
        /// </summary>
        public string TargetServerRelativeUrl { get; internal set; }

        /// <summary>
        /// Url of the site containing the file requested to be classified and extracted
        /// </summary>
        public string TargetSiteUrl { get; internal set; }

        /// <summary>
        /// Server relative url of the web containing the file requested to be classified and extracted
        /// </summary>
        public string TargetWebServerRelativeUrl { get; internal set; }
    }
}
