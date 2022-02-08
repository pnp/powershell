using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Contains information about an ongoing SharePoint Online site collection rename
    /// </summary>
    public class SPOSiteRenameJob
    {
        /// <summary>
        /// State the rename process is in
        /// </summary>
        public string JobState { get; set; }

        /// <summary>
        /// Id of the site that is being renamed
        /// </summary>
        public Guid? SiteId { get; set; }

        /// <summary>
        /// Unique identifier of the rename job
        /// </summary>
        public Guid? JobId { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Person or process having initiated the rename
        /// </summary>
        public string TriggeredBy { get; set; }

        /// <summary>
        /// Error code, if any
        /// </summary>
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Error description, if any
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Url of the site collection before the rename
        /// </summary>
        public string SourceSiteUrl { get; set; }

        /// <summary>
        /// Url of the site collection after the rename
        /// </summary>
        public string TargetSiteUrl { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public object TargetSiteTitle { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public int? Option { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public object Reserve { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public object SkipGestures { get; set; }        
    }
}