using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Mail
{
    /// <summary>
    /// Contains a Graph Message Recipient E-mail Address
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/recipient</remarks>
    public class EmailAddress
    {
        /// <summary>
        /// The display name of the person or entity
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The email address of the person or entity
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}