using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class DataSource
    {
        [JsonPropertyName("entitySetName")]
        public string EntitySetName { get; set; }

        [JsonPropertyName("logicalName")]
        public string LogicalName { get; set; }
    }
}
