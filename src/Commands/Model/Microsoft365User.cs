using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Contains the information of an Entra ID user object
    /// </summary>
    public class Microsoft365User
    {
        /// <summary>
        /// Unique identifier of this user object in Entra ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// User's user principal name
        /// </summary>
        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// User's display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// User's given name
        /// </summary>
        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        /// <summary>
        /// User's surname
        /// </summary>
        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        /// <summary>
        /// User's e-mail address
        /// </summary>

        [JsonPropertyName("mail")]
        public string Email { get; set; }

        /// <summary>
        /// User's mobile phone number
        /// </summary>
        [JsonPropertyName("mobilePhone")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// User's preferred language in ISO 639-1 standard notation
        /// </summary>
        [JsonPropertyName("preferredLanguage")]
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// User's job title
        /// </summary>
        [JsonPropertyName("jobTitle")]
        public string JobTitle { get; set; }

        /// <summary>
        /// User's business phone numbers
        /// </summary>
        [JsonPropertyName("businessPhones")]
        public string[] BusinessPhones { get; set; }

        /// <summary>
        /// User's job title
        /// </summary>
        [JsonPropertyName("userType")]
        public Enums.UserType? UserType { get; set; }

        /// <summary>
        /// Location from which Microsoft 365 will mainly be used
        /// </summary>
        [JsonPropertyName("usageLocation")]
        public string UsageLocation { get; set; }

        /// <summary>
        /// Aliases set on the mailbox of this user
        /// </summary>
        [JsonPropertyName("proxyAddresses")]
        public string[] ProxyAddresses { get; set; }       
    }
}