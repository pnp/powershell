using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    public class AuditInitiatedBy
    {
        [JsonPropertyName("user")]
        public AuditUser User { get; set; }

        [JsonPropertyName("app")]
        public object app { get; set; }
    }
}
