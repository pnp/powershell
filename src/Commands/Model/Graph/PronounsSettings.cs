using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph
{
    /// <summary>
    /// Contains a pronounsSettings property information
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/pronounssettings</remarks>
    public class PronounsSettings
    {
        /// <summary>
        /// isEnabledInOrganization property name
        /// </summary>
        [JsonPropertyName("isEnabledInOrganization")]
        public bool? IsPronounsEnabledInOrganization { get; set; }

    }
}
