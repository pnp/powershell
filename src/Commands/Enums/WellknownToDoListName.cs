using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the well-known Microsoft To Do list names returned by Microsoft Graph.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WellknownToDoListName
    {
        /// <summary>
        /// Indicates that the list is not a well-known list.
        /// </summary>
        None,

        /// <summary>
        /// Indicates the default Microsoft To Do list.
        /// </summary>
        DefaultList,

        /// <summary>
        /// Indicates the flagged emails list.
        /// </summary>
        FlaggedEmails,

        /// <summary>
        /// Indicates a value returned by Microsoft Graph that is not known by this version of PnP PowerShell.
        /// </summary>
        UnknownFutureValue
    }
}
