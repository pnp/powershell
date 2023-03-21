using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Mail
{
    /// <summary>
    /// Contains an e-mail message to send through Microsoft Graph
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/user-sendmail</remarks>
    public class SendMailMessage
    {
        /// <summary>
        /// The message to send
        /// </summary>
        [JsonPropertyName("message")]
        public Message Message { get; set; }
        
        /// <summary>
        /// Indicates whether to save the message in Sent Items. Specify it only if the parameter is false; default is true. Optional.
        /// </summary>
        [JsonPropertyName("saveToSentItems")]
        public bool SaveToSentItems { get; set; }
    }
}