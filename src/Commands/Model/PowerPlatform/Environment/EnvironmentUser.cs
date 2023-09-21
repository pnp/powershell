using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Information on a user having created or modified a Power Automate environment
    /// </summary>
    public class EnvironmentUser
    {
        /// <summary>
        /// Id of the user object in Entra ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Friendly displayname for the user
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// E-mail address of the user
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        /// <summary>
        /// Type of user
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Id of the tenant
        /// </summary>
        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; }

        /// <summary>
        /// User Principal Name (UPN) of the user
        /// </summary>
        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }
}