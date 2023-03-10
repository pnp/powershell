using PnP.PowerShell.Commands.Enums;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Mail
{
    /// <summary>
    /// Contains a Graph Message Body
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/itembody</remarks>
    public class Body
    {
        /// <summary>
        /// Contents the contents of the message body
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Contains the type of the message body
        /// </summary>
        [JsonPropertyName("contentType")]
        public MessageBodyContentType? ContentType { get; set; }
    }
}