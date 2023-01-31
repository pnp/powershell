namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// The different types of sites for which a Site Design can be created. See https://learn.microsoft.com/sharepoint/dev/declarative-customization/customize-default-site-design#apply-a-site-script-to-a-default-site-template.
    /// </summary>
    public enum SiteWebTemplate
    {
        /// <summary>
        /// Modern Teamsite without a Microsoft 365 Group
        /// </summary>
        GrouplessTeamSite = 1,

        /// <summary>
        /// Modern Teamsite with a Microsoft 365 Group
        /// </summary>
        TeamSite = 64,

        /// <summary>
        /// Modern Communication site
        /// </summary>
        CommunicationSite = 68,

        /// <summary>
        /// Site created as part of a private Teams channel
        /// </summary>
        ChannelSite = 69
    }
}