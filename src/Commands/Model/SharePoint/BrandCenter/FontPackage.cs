using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.SharePoint.BrandCenter
{
    /// <summary>
    /// Represents a font package in the Brand Center
    /// </summary>
    public class FontPackage
    {
        /// <summary>
        /// Unique identifier of the font package
        /// </summary>
        [JsonPropertyName("ID")]
        public Guid? Id { get; set; }
        /// <summary>
        /// Name of the font package
        /// </summary>
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Defines where the font package is stored
        /// </summary>
        [JsonPropertyName("Store")]
        public Store Store { get; set; }

        /// <summary>
        /// The JSON definition of the font package choices
        /// </summary>
        [JsonPropertyName("PackageJson")]
        public string PackageJson { get; set; }

        /// <summary>
        /// Indication if the font package is valid
        /// </summary>
        [JsonPropertyName("IsValid")]
        public bool? IsValid { get; set; }

        /// <summary>
        /// Indication if the font package is hidden
        /// </summary>
        [JsonPropertyName("IsHidden")]
        public bool? IsHidden { get; set; }
    }
}