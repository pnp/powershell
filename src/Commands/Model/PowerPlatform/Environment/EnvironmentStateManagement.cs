using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on the management state of a Power Automate environment
    /// </summary>
    public class EnvironmentStateManagement
    {
        /// <summary>
        /// Indicator if the runtime is Enabled
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}