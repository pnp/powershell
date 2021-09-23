using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// The current health of a service
    /// </summary>
    public class ServiceHealthCurrent
    {
        /// <summary>
        /// Id of the service
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the service
        /// </summary>
        [JsonPropertyName("service")]
        public string Name { get; set; }

        /// <summary>
        /// Current status of the service
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }     
    }
}


