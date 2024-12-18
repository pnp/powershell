using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.VivaEngage
{
    public class VivaEngageCommunity
    {
        /// <summary>
        /// Unique identifier of the Viva Engage community
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the Viva Engage community
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the Viva Engage community
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Privacy setting of the Viva Engage community
        /// </summary>
        [JsonPropertyName("privacy")]
        public CommunityPrivacy Privacy { get; set; }

        /// <summary>
        /// GroupId of the Viva Engage community
        /// </summary>
        [JsonPropertyName("groupId")]
        public Guid GroupId { get; set; }
    }

    public enum CommunityPrivacy
    {
        Public,
        Private
    }

    public class VivaEngageProvisioningResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonPropertyName("lastActionDateTime")]
        public DateTime LastActionDateTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("statusDetail")]
        public string StatusDetail { get; set; }

        [JsonPropertyName("resourceLocation")]
        public string ResourceLocation { get; set; }

        [JsonPropertyName("operationType")]
        public string OperationType { get; set; }

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }
    }

}
