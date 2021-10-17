using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// All properties regarding the looks of the header on a site
    /// </summary>
    public class SharePointWebHeader
    {
        /// <summary>
        /// Server relative path to the image to use as the site logo
        /// </summary>
        public string SiteLogoUrl { get; set; }

        /// <summary>
        /// Layout to apply for the site header
        /// </summary>
        public HeaderLayoutType HeaderLayout { get; set; }

        /// <summary>
        /// Emphasis type to use for the bar shown at the top of the site under the site title and logo
        /// </summary>
        public SPVariantThemeType? HeaderEmphasis { get; set; }

        /// <summary>
        /// Indicates if the site title should be hidden at the top of the site
        /// </summary>
        public bool? HideTitleInHeader { get; set; }

        /// <summary>
        /// Server relative path to the image to use behind the site header
        /// </summary>
        public string HeaderBackgroundImageUrl { get; set; }

        /// <summary>
        /// Indicates if the site logo and title should be shown on the left, in the middle or on the right
        /// </summary>
        public LogoAlignment? LogoAlignment { get; set; }
    }
}