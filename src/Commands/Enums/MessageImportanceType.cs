namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Contains the types of importance that can be used in a message
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/graph/api/resources/message#properties</remarks>
    public enum MessageImportanceType
    {
        /// <summary>
        /// Low priority message
        /// </summary>
        Low,

        /// <summary>
        /// Normal priority message
        /// </summary>
        Normal,

        /// <summary>
        /// High priority message
        /// </summary>
        High
    }
}