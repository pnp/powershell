using System;
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
