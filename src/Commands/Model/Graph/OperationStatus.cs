using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph
{
    /// <summary>
    /// Model for an operation status in Microsoft Graph
    /// </summary>
    public class OperationStatus
    {
        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The status of the operation
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The error message, if anything went wrong
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
