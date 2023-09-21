using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    public class ServicePrincipalAppRoleAssignment
    {
        [JsonPropertyName("appRoleId")]
        public Guid? AppRoleId { get; set; }

        [JsonIgnore]
        public string AppRoleName { get; set; } 

        [JsonPropertyName("createdDateTime")]
        public DateTime? CreatedDateTime { get; set; }

        [JsonPropertyName("deletedDateTime")]
        public DateTime? DeletedDateTime { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("principalDisplayName")]
        public string PrincipalDisplayName { get; set; }

        [JsonPropertyName("principalId")]
        public string PrincipalId { get; set; }

        [JsonPropertyName("principalType")]
        public string PrincipalType { get; set; }  

        [JsonPropertyName("resourceDisplayName")]
        public string ResourceDisplayName { get; set; }

        [JsonPropertyName("resourceId")]
        public Guid? ResourceId { get; set; }
    }
}
