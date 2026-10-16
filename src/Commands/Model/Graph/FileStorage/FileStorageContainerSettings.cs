namespace PnP.PowerShell.Commands.Model.Graph.FileStorage
{
    /// <summary>
    /// Settings of a SharePoint Embedded container, as returned by Microsoft Graph
    /// See <a href="https://learn.microsoft.com/graph/api/resources/filestoragecontainersettings">Graph Reference</a>
    /// </summary>
    public class FileStorageContainerSettings
    {
        /// <summary>
        /// Whether optical character recognition is performed on new and updated documents
        /// </summary>
        public bool? IsOcrEnabled { get; set; }

        /// <summary>
        /// Whether versioning is enabled for items in the container
        /// </summary>
        public bool? IsItemVersioningEnabled { get; set; }

        /// <summary>
        /// Maximum number of major versions kept of each item in the container
        /// </summary>
        public int? ItemMajorVersionLimit { get; set; }
    }
}
