using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.Purview
{
    public class RetentionDuration
    {
        /// <summary>
        /// The type of the data.
        /// </summary>
        [JsonPropertyName("@odata.type")]
        public string ODataType { get; set; } = "#microsoft.graph.security.retentionDurationInDays";
        /// <summary>
        /// Number of days.
        /// </summary>
        public int Days { get; set; }
    }
}
