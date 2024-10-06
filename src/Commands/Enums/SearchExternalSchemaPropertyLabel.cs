namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the possible well-known tags that can be assigned to properties in the schema definition of an external search connection to make Microsoft Search understand its content better
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-property?view=graph-rest-1.0#properties"/>
    public enum SearchExternalSchemaPropertyLabel : short
    {
        /// <summary>
        /// Property represents the title of the item
        /// </summary>
        Title,

        /// <summary>
        /// Property represents a link to the item
        /// </summary>
        Url,

        /// <summary>
        /// Property represents the author of the item
        /// </summary>
        CreatedBy,

        /// <summary>
        /// Property represents the last person who modified the item
        /// </summary>
        LastModifiedBy,

        /// <summary>
        /// Property represents the authors of the item
        /// </summary>
        Authors,

        /// <summary>
        /// Property represents the date and time the item was created
        /// </summary>
        CreatedDateTime,

        /// <summary>
        /// Property represents the date and time the item was last modified
        /// </summary>
        LastModifiedDateTime,

        /// <summary>
        /// Property represents the name of the file
        /// </summary>
        FileName,

        /// <summary>
        /// Property represents the extension of the file
        /// </summary>
        FileExtension,

        /// <summary>
        /// Property stating a value has been returned that is not amongst the basic set of enums
        /// </summary>
        unknownFutureValue,

        /// <summary>
        /// Property represents the icon to be shown with the item
        /// </summary>
        IconUrl
    }
}
