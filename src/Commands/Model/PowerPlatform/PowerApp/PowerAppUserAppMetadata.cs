using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppUserAppMetadata
    {
        [JsonPropertyName("favorite")]
        public string Favorite { get; set; }

        [JsonPropertyName("includeInAppsList")]
        public bool? IncludeInAppsList { get; set; }
    }
}
