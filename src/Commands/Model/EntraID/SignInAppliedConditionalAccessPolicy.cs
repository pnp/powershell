using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    public class SignInAppliedConditionalAccessPolicy
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("enforcedGrantControls")]
        public List<object> EnforcedGrantControls { get; set; }

        [JsonPropertyName("enforcedSessionControls")]
        public List<object> EnforcedSessionControls { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
