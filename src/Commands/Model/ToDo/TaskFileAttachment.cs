using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ToDo
{
    public class TaskFileAttachment
    {
        [JsonPropertyName("@odata.type")]
        public string ODataType { get; set; } = "#microsoft.graph.taskFileAttachment";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyName("contentBytes")]
        public string ContentBytes { get; set; }

        [JsonPropertyName("lastModifiedDateTime")]
        public DateTime? LastModifiedDateTime { get; set; }

        [JsonPropertyName("size")]
        public int? Size { get; set; }
    }
}
