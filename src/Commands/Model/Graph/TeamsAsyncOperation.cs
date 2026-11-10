namespace PnP.PowerShell.Commands.Model.Graph
{
    /// <summary>
    /// Represents a Microsoft Teams async operation returned by Microsoft Graph.
    /// </summary>
    public class TeamsAsyncOperation
    {
        /// <summary>
        /// Unique identifier of the operation.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Current status of the operation.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Identifier of the team that was created or modified by the operation.
        /// </summary>
        public string TargetResourceId { get; set; }

        /// <summary>
        /// Location of the team that was created or modified by the operation.
        /// </summary>
        public string TargetResourceLocation { get; set; }

        /// <summary>
        /// Error details returned when the operation fails.
        /// </summary>
        public TeamsAsyncOperationError Error { get; set; }
    }

    /// <summary>
    /// Represents Teams async operation error details returned by Microsoft Graph.
    /// </summary>
    public class TeamsAsyncOperationError
    {
        /// <summary>
        /// Error code returned by Microsoft Graph.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error message returned by Microsoft Graph.
        /// </summary>
        public string Message { get; set; }
    }
}
