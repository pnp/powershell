using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class DatabaseDetails
    {
        [JsonPropertyName("referenceType")]
        public string ReferenceType { get; set; }

        [JsonPropertyName("environmentName")]
        public string EnvironmentName { get; set; }

        [JsonPropertyName("linkedEnvironmentMetadata")]
        public PowerAppLinkedEnvironmentMetadata LinkedEnvironmentMetadata { get; set; }

        [JsonPropertyName("overrideValues")]
        public PowerAppOverrideValues OverrideValues { get; set; }
    }
}
