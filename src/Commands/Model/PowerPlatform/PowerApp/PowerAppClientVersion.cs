using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppClientVersion
    {
        [JsonPropertyName("major")]
        public long Major { get; set; }

        [JsonPropertyName("minor")]
        public long Minor { get; set; }

        [JsonPropertyName("build")]
        public long Build { get; set; }

        [JsonPropertyName("revision")]
        public long Revision { get; set; }

        [JsonPropertyName("majorRevision")]
        public long MajorRevision { get; set; }

        [JsonPropertyName("minorRevision")]
        public long MinorRevision { get; set; }
    }
}
