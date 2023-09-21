using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    public class SignInStatus
    {
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("failureReason")]
        public string FailureReason { get; set; }

        [JsonPropertyName("additionalDetails")]
        public object AdditionalDetails { get; set; }
    }
}
