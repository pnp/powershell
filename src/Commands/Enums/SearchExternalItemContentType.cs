namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the possible content types for the search external item to define the type of content it contains
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-externalitemcontent#properties"/>
    public enum SearchExternalItemContentType : short
    {
        /// <summary>
        /// The content is HTML
        /// </summary>
        Html = 0,

        /// <summary>
        /// The content is plain text
        /// </summary>
        Text = 1
    }
}
