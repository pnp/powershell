using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on the features of a Power Automate environment
    /// </summary>
    public class EnvironmentFeatures
    {
        /// <summary>
        /// Boolean indicating if the environment is enabeld for OpenAPI
        /// </summary>
        [JsonPropertyName("isOpenApiEnabled")]
        public bool? IsOpenApiEnabled { get; set; }
    }
}