using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// The progress for generating the file version expiration report.
    /// </summary>
    public class FileVersionExpirationReportJobStatus
    {
        /// <summary>
        /// The URL to the site or library that the report is for
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The URL of the report file
        /// </summary>
        [JsonPropertyName("report_url")]
        public string ReportUrl { get; set; }

        /// <summary>
        /// The request status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The error message if any
        /// </summary>
        [JsonPropertyName( "error_message")]
        public string ErrorMessage { get; set; }
    }
}
