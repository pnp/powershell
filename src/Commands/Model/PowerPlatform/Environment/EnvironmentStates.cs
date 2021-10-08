using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on the states of a Power Automate environment
    /// </summary>
    public class EnvironmentStates
    {
        /// <summary>
        /// Information on the management state of a Power Automate environment
        /// </summary>
        [JsonPropertyName("management")]
        public EnvironmentStateManagement Management { get; set; }

        /// <summary>
        /// Information on the runtime state of a Power Automate environment
        /// </summary>
        [JsonPropertyName("runtime")]
        public EnvironmentStateRuntime Runtime { get; set; }
    }
}