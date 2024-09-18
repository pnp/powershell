namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the possible types of properties in the schema definition of an external search connection
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-property?view=graph-rest-1.0#properties"/>
    public enum SearchExternalSchemaPropertyType : short
    {
        /// <summary>
        /// Property contains a text value
        /// </summary>
        String,

        /// <summary>
        /// Property contains a numeric value
        /// </summary>
        Int64,

        /// <summary>
        /// Property contains a floating point value
        /// </summary>
        Double,

        /// <summary>
        /// Property contains a date and time value
        /// </summary>
        DateTime,

        /// <summary>
        /// Property contains a boolean value
        /// </summary>
        Boolean,

        /// <summary>
        /// Property contains a collection of text values
        /// </summary>
        StringCollection,

        /// <summary>
        /// Property contains a collection of numeric values
        /// </summary>
        Int64Collection,

        /// <summary>
        /// Property contains a collection of floating point values
        /// </summary>
        DoubleCollection,

        /// <summary>
        /// Property contains a collection of date and time values
        /// </summary>
        DateTimeCollection        
    }
}
