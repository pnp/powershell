using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information on who created a Microsoft Power Automate Flow
    /// </summary>
    public class FlowCreator
    {
        /// <summary>
        /// GUID of the Microsoft 365 tenant / Entra ID in which the Flow has been created
        /// </summary>
        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; }

        /// <summary>
        /// Unique ID of the object that created the Flow. If created by a user, this will be equal to <see cref="UserId" />.
        /// </summary>
        [JsonPropertyName("objectId")]
        public string ObjectId { get; set; }

        /// <summary>
        /// Unique ID of the user from the user source that created the Flow, i.e. the user GUID in Entra ID
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Type of user having created the Flow, i.e. ActiveDirectory
        /// </summary>
        [JsonPropertyName("userType")]
        public string UserType { get; set; }
    }
}