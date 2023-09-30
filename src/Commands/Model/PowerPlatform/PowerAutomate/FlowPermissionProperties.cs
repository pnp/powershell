using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    public class FlowPermissionProperties
    {
        /// <summary>
        /// User role name.
        /// </summary>
        [JsonPropertyName("roleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// Permission type of the user
        /// </summary>
        [JsonPropertyName("permissionType")]
        public string PermissionType { get; set; }

        /// <summary>
        /// User principal, Usually Id & Type
        /// </summary>
        [JsonPropertyName("principal")]
        public FlowPermissionPrincipal Principal { get; set; }
    }
}