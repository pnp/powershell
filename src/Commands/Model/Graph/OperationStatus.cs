using System;
using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// The status of the operation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The error message, if anything went wrong
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
