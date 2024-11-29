using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.Copilot
{
    /// <summary>
    /// Contains a profile card property information
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/copilotadminlimitedmode-get</remarks>
    public class CopilotAdminLimitedMode
    {
        /// <summary>
        /// The ID of a Microsoft Entra group to which the value of the isEnabledForGroup property is applied value. If NULL, it applies to all users.
        /// </summary>
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }
        
        /// <summary>
        /// Boolean indicating if the user to be in limited mode for Copilot in Teams meetings is enabled
        /// </summary>
        [JsonPropertyName("isEnabledForGroup")]
        public bool IsEnabledForGroup { get; set; }
    }
}
