using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Mail
{
    public class MessageAttachmentOptions
    {
        [JsonPropertyName("@odata.type")]
        public string Type { get; set; } = "#microsoft.graph.fileAttachment";
        /// <summary>
        /// Name of the attachment
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Content-Type of the attachment
        /// </summary>
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; } = "text/plain";

        /// <summary>
        /// Bytes of the attachment
        /// </summary>
        [JsonPropertyName("contentBytes")]
        public string ContentBytes { get; set; }
    }
}
