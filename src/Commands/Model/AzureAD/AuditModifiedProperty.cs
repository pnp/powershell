using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AuditModifiedProperty
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("oldValue")]
        public object OldValue { get; set; }
        [JsonPropertyName("newValue")]
        public string NewValue { get; set; }
    }
}
