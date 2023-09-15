using PnP.Framework.Extensions;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnector
    {
        /// <summary>
        /// Name of the connector as its GUID
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Unique identifier of this connector.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// Type of object, typically Microsoft.PowerApps/apis
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        /// <summary>
        /// Additional information on the Connector
        /// </summary>
        [JsonPropertyName("properties")]
        public PowerPlatformConnectorProperties Properties { get; set; }
        public bool IsCustomConnector
        {
            get { return Properties.isCustomApi; }
        }
        public string DisplayName
        {
            get { return Properties.displayName; }
        }
    }
}
