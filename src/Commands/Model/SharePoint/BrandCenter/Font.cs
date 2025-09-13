using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Model.SharePoint.BrandCenter
{
    /// <summary>
    /// Represents a font in the Brand Center
    /// </summary>
    public class Font
    {
        /// <summary>
        /// Unique identifier of the font
        /// </summary>
        [JsonPropertyName("ID")]
        public string Id { get; set; }
        /// <summary>
        /// Name of the font
        /// </summary>
        [JsonPropertyName("_SPFontFamilyName")]
        public string Name { get; set; }

        /// <summary>
        /// The type of font styles in the font
        /// </summary>
        [JsonPropertyName("_SPFontFaces")]
        public List<string> FontStyles { get; set; }

        /// <summary>
        /// The filename of the font
        /// </summary>
        [JsonPropertyName("FileLeafRef")]
        public string FileName { get; set; }

        /// <summary>
        /// Indication if the font is visible
        /// </summary>
        [JsonPropertyName("_SPFontVisible")]
        public string IsVisible { get; set; }

        /// <summary>
        /// Indication if the font is hidden
        /// </summary>
        [JsonIgnore]
        public bool IsHidden => string.Equals(IsVisible, "No", StringComparison.OrdinalIgnoreCase);
    }
}