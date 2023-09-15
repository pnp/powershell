using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains information of Microsoft Power Automate Flow owners
    /// </summary>
    public class FlowPermission
    {
        /// <summary>
        /// Name of the Flow as its Flow GUID
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique identifier of this Flow.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of object, typically Microsoft.ProcessSimple/environments/flows/permissions
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Additional information on the Flow owners
        /// </summary>
        [JsonPropertyName("properties")]
        public FlowPermissionProperties Properties { get; set; }
    }
}