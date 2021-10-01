using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on a Power Automate environment
    /// </summary>
    public class Environment
    {
        /// <summary>
        /// Internal name of the Power Automate environment
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Information on in which region the environment is hosted
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }

        /// <summary>
        /// Internal identifier for the type of environment
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Full path to the identifier of this Power Automate environment
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Additional properties on the environment
        /// </summary>
        [JsonPropertyName("properties")]
        public EnvironmentProperties Properties { get; set; }
    }
}