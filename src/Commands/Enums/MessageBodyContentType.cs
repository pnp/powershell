namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the types of content that can be used in a message body
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/itembody#properties</remarks>
    public enum MessageBodyContentType
    {
        /// <summary>
        /// Message will be in plain text
        /// </summary>
        Text,

        /// <summary>
        /// Message will be formatted using HTML
        /// </summary>
        Html
    }
}