

using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    public class FlowPermissionPrincipal
    {
        /// <summary>
        /// Principal ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Principal type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}