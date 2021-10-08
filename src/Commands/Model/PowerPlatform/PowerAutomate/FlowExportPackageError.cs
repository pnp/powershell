using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains the error details when requesting an export of a Flow package
    /// </summary>
    public class FlowExportPackageError
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Description of the error
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}