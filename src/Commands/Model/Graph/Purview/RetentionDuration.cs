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
        /// Number of days. Not present when the duration is retentionDurationForever. Microsoft Graph has been observed returning this both as a number and as a string, so both are accepted.
        /// </summary>
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? Days { get; set; }
    }
}
