namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the options for showing fields in the Filter Pane
    /// </summary>
    /// <remarks>See https://learn.microsoft.com/en-us/previous-versions/office/sharepoint-server/mt844959(v=office.15) for more information</remarks>
    public enum ShowInFiltersPaneStatus
    {
        /// <summary>
        /// Have SharePoint define if the field should be shown in the filter pane
        /// </summary>
        Auto,

        /// <summary>
        /// Always show the field in the filter pane
        /// </summary>
        Pinned,

        /// <summary>
        /// Never show the field in the filter pane
        /// </summary>
        Removed
    }
}
