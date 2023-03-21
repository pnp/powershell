using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.Mail
{
    /// <summary>
    /// Contains a Graph Message
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/message#properties</remarks>
    public class Message
    {
        /// <summary>
        /// Unique identifier for the message
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// The To: recipients for the message
        /// </summary>
        [JsonPropertyName("toRecipients")]
        public List<Recipient> ToRecipients { get; set; }

        /// <summary>
        /// The Cc: recipients for the message
        /// </summary>
        [JsonPropertyName("ccRecipients")]
        public List<Recipient> CcRecipients { get; set; }

        /// <summary>
        /// The Bcc: recipients for the message
        /// </summary>
        [JsonPropertyName("bccRecipients")]
        public List<Recipient> BccRecipients { get; set; }

        /// <summary>
        /// The account that is actually used to generate the message. In most cases, this value is the same as the from property. You can set this property to a different value when sending a message from a shared mailbox, for a shared calendar, or as a delegate. In any case, the value must correspond to the actual mailbox used.
        /// </summary>
        [JsonPropertyName("sender")]
        public Recipient Sender { get; set; }

        /// <summary>
        /// The email addresses to use when replying
        /// </summary>
        [JsonPropertyName("replyTo")]
        public List<Recipient> ReplyTo { get; set; }       

        /// <summary>
        /// The subject of the message
        /// </summary>
        [JsonPropertyName("subject")]       
        public string Subject { get; set; }

        /// <summary>
        /// The message to send
        /// </summary>
        [JsonPropertyName("body")]
        public Body MessageBody { get; set; }

        /// <summary>
        /// The categories associated with the message
        /// </summary>
        [JsonPropertyName("categories")]
        public string[] Categories { get; set; }

        /// <summary>
        /// The importance of the message
        /// </summary>
        [JsonPropertyName("importance")]
        public MessageImportanceType? Importance { get; set; }
    }
}