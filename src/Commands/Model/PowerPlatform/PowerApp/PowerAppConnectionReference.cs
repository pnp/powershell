using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppConnectionReference
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("iconUri")]
        public Uri IconUri { get; set; }

        [JsonPropertyName("dataSources")]
        public string[] DataSources { get; set; }

        [JsonPropertyName("dependencies")]
        public Guid[] Dependencies { get; set; }

        [JsonPropertyName("dependents")]
        public Guid[] Dependents { get; set; }

        [JsonPropertyName("isOnPremiseConnection")]
        public bool? IsOnPremiseConnection { get; set; }

        [JsonPropertyName("bypassConsent")]
        public bool? BypassConsent { get; set; }

        [JsonPropertyName("apiTier")]
        public string ApiTier { get; set; }

        [JsonPropertyName("isCustomApiConnection")]
        public bool? IsCustomApiConnection { get; set; }

        [JsonPropertyName("actions")]
        public string[] Actions { get; set; }

        [JsonPropertyName("nestedActions")]
        public PowerAppNestedAction[] NestedActions { get; set; }

        [JsonPropertyName("gatewayObjectIdHint")]
        public Guid? GatewayObjectIdHint { get; set; }

        [JsonPropertyName("sharedConnectionId")]
        public string SharedConnectionId { get; set; }

        [JsonPropertyName("authenticationType")]
        public string AuthenticationType { get; set; }

        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }


    }
}
