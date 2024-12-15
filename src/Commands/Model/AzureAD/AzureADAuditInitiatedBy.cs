using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADAuditInitiatedBy
    {
        [JsonPropertyName("user")]
        public AzureADAuditUser User { get; set; }
        [JsonPropertyName("app")]
        public object app { get; set; }
    }
}
