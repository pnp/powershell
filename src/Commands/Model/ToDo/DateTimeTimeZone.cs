using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ToDo
{
    public class DateTimeTimeZone
    {
        [JsonPropertyName("dateTime")]
        public string DateTime { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }
    }
}
