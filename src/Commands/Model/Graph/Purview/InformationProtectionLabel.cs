using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.Purview
{
    /// <summary>
    /// Describes the information protection label that details how to properly apply a sensitivity label to information. The informationProtectionLabel resource describes the configuration of sensitivity labels that apply to a user or tenant.
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/informationprotectionlabel"/>
    public class InformationProtectionLabel
    {
        /// <summary>
        /// The label ID is a globally unique identifier (GUID)
        /// </summary>
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// The plaintext name of the label.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The admin-defined description for the label.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The color that the UI should display for the label, if configured.
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// The sensitivity value of the label, where lower is less sensitive.
        /// </summary>
        [JsonPropertyName("sensitivity")]
        public int Sensitivity { get; set; }

        /// <summary>
        /// The tooltip that should be displayed for the label in a UI.
        /// </summary>
        [JsonPropertyName("tooltip")]
        public string Tooltip { get; set; }

        /// <summary>
        /// Indicates whether the label is active or not. Active labels should be hidden or disabled in UI.
        /// </summary>
        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// The parent label associated with a child label. Null if label has no parent.    
        /// </summary>
        [JsonPropertyName("parent")]
        public object Parent { get; set; }
    }
}