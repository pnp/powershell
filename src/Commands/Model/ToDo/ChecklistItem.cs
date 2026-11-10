using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ToDo
{
    public class ChecklistItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("isChecked")]
        public bool? IsChecked { get; set; }

        [JsonPropertyName("checkedDateTime")]
        public System.DateTime? CheckedDateTime { get; set; }

        [JsonPropertyName("createdDateTime")]
        public System.DateTime? CreatedDateTime { get; set; }
    }
}
