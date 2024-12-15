using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.ProfileCard
{
    /// <summary>
    /// Contains a profile card property annotation
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/en-us/graph/api/resources/profilecardannotation</remarks>
    public class ProfileCardPropertyAnnotation
    {
        /// <summary>
        /// Unique identifier for the message
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        
        /// <summary>
        /// The To: recipients for the message
        /// </summary>
        [JsonPropertyName("localizations")]
        public List<ProfileCardPropertyLocalization> Localizations { get; set; }
    }
}
