using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class DatabaseReferences
    {
        [JsonPropertyName("default.cds")]
        public PowerAppDefaultCds DefaultCds { get; set; }
    }
}
