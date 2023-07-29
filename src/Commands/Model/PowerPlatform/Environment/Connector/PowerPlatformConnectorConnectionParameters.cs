using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnectorConnectionParameters
    {
        [JsonPropertyName("token")]
        public PowerPlatformConnectorToken token { get; set; }

        [JsonPropertyName("token:TenantId")]
        public PowerPlatformConnectorTokenTenantId tokenTenantId { get; set; }
    }

}
