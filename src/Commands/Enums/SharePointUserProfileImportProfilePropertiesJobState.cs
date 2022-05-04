namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// The states a SharePoint Online User Profile import job can be in
    /// </summary>
    public enum SharePointUserProfileImportProfilePropertiesJobState
    {
        /// <summary>
        /// State is unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The file has been submitted to SharePoint Online for processing
        /// </summary>
        Submitted = 1,

        /// <summary>
        /// The file is currently being processed to validate if it can be used
        /// </summary>
        Processing = 2,

        /// <summary>
        /// The file is queued and being executed
        /// </summary>
        Queued = 3,

        /// <summary>
        /// The import process has completed successfully
        /// </summary>
        Succeeded = 4,

        /// <summary>
        /// The import process has failed to complete
        /// </summary>
        Error = 5,

        /// <summary>
        /// The import process will not start
        /// </summary>
        WontStart = 99
    }
}