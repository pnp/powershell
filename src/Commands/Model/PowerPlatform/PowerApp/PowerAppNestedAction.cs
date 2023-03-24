using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppNestedAction
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiName")]
        public string ApiName { get; set; }

        [JsonPropertyName("actionName")]
        public string ActionName { get; set; }

        [JsonPropertyName("referencedResources")]
        public PowerAppReferencedResource[] ReferencedResources { get; set; }
    }
}
