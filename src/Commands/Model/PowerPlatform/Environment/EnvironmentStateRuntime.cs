using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on the runtime state of a Power Automate environment
    /// </summary>
    public class EnvironmentStateRuntime
    {
        /// <summary>
        /// Indicator if the runtime is Ready
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}