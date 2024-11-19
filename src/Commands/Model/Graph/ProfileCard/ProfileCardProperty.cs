using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.Graph.ProfileCard
{
    /// <summary>
    /// Contains a profile card property information
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/en-us/graph/api/resources/profilecardproperty</remarks>
    public class ProfileCardProperty
    {
        /// <summary>
        /// Directory property name
        /// </summary>
        [JsonPropertyName("directoryPropertyName")]
        public string DirectoryPropertyName { get; set; }
        
        /// <summary>
        /// Annotations
        /// </summary>
        [JsonPropertyName("annotations")]
        public List<ProfileCardPropertyAnnotation> Annotations { get; set; }
    }
}
