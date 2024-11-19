using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.Graph.ProfileCard
{
    /// <summary>
    /// Contains a profile card property localization
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/en-us/graph/api/resources/displaynamelocalization</remarks>
    public class ProfileCardPropertyLocalization
    {
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Language tag
        /// <example>de</example>
        /// <example>nl-NL</example>
        /// </summary>
        [JsonPropertyName("languageTag")]
        public string LanguageTag { get; set; }
    }
}
