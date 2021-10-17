using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Response to invoking a site script action containing its status
    /// </summary>
    public class InvokeSiteScriptActionResponse
    {
        /// <summary>
        /// Error code of executing the action. 0 means no error occurred.
        /// </summary>
        [JsonPropertyName("ErrorCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Outcome of executing the action. 0 means successful.
        /// </summary>
        [JsonPropertyName("Outcome")]
        public string Outcome { get; set; }

        /// <summary>
        /// Optional text describing the outcome of the action
        /// </summary>
        [JsonPropertyName("OutcomeText")]
        public string OutcomeText { get; set; }

        /// <summary>
        /// The full URL to the object that has been created because of this action, if applicable, otherwise null
        /// </summary>
        [JsonPropertyName("Target")]
        public string Target { get; set; }

        /// <summary>
        /// The Id of the object that has been created because of this action, if applicable, otherwise null
        /// </summary>
        [JsonPropertyName("TargetId")]
        public string TargetId { get; set; }

        /// <summary>
        /// Title of the action in the site script
        /// </summary>
        [JsonPropertyName("Title")]
        public string Title { get; set; }
    }
}