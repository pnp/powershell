using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.Purview
{
    public class InformationProtectionLabel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("sensitivity")]
        public int Sensitivity { get; set; }

        [JsonPropertyName("tooltip")]
        public string Tooltip { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("parent")]
        public object Parent { get; set; }
    }
}