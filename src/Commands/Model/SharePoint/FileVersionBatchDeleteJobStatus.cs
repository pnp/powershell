using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// The progress for the request that batch delete file versions
    /// </summary>
    public class FileVersionBatchDeleteJobStatus
    {
        /// <summary>
        /// Site/Library Url
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
        /// The error meassage if there is any
        /// </summary>
        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }

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
        /// The batch delete mode
        /// </summary>
        [JsonPropertyName("BatchDeleteMode")]
        public string BatchDeleteMode { get; set; }

        /// <summary>
        /// Delete older than when BatchDeleteMode is DeleteOlderThanDays
        /// </summary>
        [JsonPropertyName("DeleteOlderThan")]
        public string DeleteOlderThan { get; set; }

        /// <summary>
        /// MajorVersionLimit when BatchDeleteMode is CountLimits
        /// </summary>
        [JsonPropertyName("MajorVersionLimit")]
        public string MajorVersionLimit { get; set; }

        /// <summary>
        /// MajorWithMinorVersionsLimit when BatchDeleteMode is CountLimits
        /// </summary>
        [JsonPropertyName("MajorWithMinorVersionsLimit")]
        public string MajorWithMinorVersionsLimit { get; set; }

        /// <summary>
        /// The files processed count
        /// </summary>
        [JsonPropertyName("FilesProcessed")]
        public string FilesProcessed { get; set; }

        /// <summary>
        /// The versions failed to process count
        /// </summary>
        [JsonPropertyName("VersionsProcessed")]
        public string VersionsProcessed { get; set; }

        /// <summary>
        /// Set versions deleted count
        /// </summary>
        [JsonPropertyName("VersionsDeleted")]
        public string VersionsDeleted { get; set; }

        /// <summary>
        /// The versions failed to delete count
        /// </summary>
        [JsonPropertyName("VersionsFailed")]
        public string VersionsFailed { get; set; }

        /// <summary>
        /// The storage released
        /// </summary>
        [JsonPropertyName("StorageReleased")]
        public string StorageReleasedInBytes { get; set; }
    }
}
