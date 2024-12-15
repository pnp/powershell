using System.Text.Json.Serialization;
namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public partial class PowerAppDefaultCds
    {
        [JsonPropertyName("databaseDetails")]
        public DatabaseDetails DatabaseDetails { get; set; }

        [JsonPropertyName("dataSources")]
        public System.Collections.Generic.Dictionary<string, DataSource> DataSources { get; set; }

        [JsonPropertyName("actions")]
        public string[] Actions { get; set; }
    }
}
