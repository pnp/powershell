using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class ServicePrincipalVerifiedPublisher
    {
        [JsonPropertyName("displayName")]
        public object DisplayName { get; set; }

        [JsonPropertyName("verifiedPublisherId")]
        public object VerifiedPublisherId { get; set; }

        [JsonPropertyName("addedDateTime")]
        public object AddedDateTime { get; set; }
    }
}
