namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp.Enums
{
    /// <summary>
    /// Contains the possible states of a App export request
    /// </summary>
    public enum PowerAppExportStatus
    {
        /// <summary>
        /// PowerApp export failed
        /// </summary>
        Failed,

        /// <summary>
        /// PowerApp exported successfully
        /// </summary>
        Succeeded,

        /// <summary>
        /// Export in progress
        /// </summary>
        Running
    }
}