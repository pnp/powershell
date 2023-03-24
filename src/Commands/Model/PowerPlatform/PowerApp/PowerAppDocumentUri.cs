using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppDocumentUri
    {
        [JsonPropertyName("value")]
        public Uri Value { get; set; }

        [JsonPropertyName("readonlyValue")]
        public Uri ReadonlyValue { get; set; }
    }
}
