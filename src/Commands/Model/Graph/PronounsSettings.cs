using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph
{
    /// <summary>
    /// Contains a pronounsSettings property information
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/en-us/graph/api/resources/pronounssettings?view=graph-rest-1.0</remarks>
    public class PronounsSettings
    {
        /// <summary>
        /// isEnabledInOrganization property name
        /// </summary>
        [JsonPropertyName("isEnabledInOrganization")]
        public bool IsPronounsEnabledInOrganization { get; set; }

    }
}
