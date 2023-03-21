using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Mail
{
    /// <summary>
    /// Contains a Graph Message Recipient
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/recipient</remarks>
    public class Recipient
    {
        /// <summary>
        /// Contents the contents of the message body
        /// </summary>
        [JsonPropertyName("emailAddress")]
        public EmailAddress EmailAddress { get; set; }
    }
}