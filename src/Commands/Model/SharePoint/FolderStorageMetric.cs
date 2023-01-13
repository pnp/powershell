
using System;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Contains storage metrics of a folder
    /// </summary>
    public class FolderStorageMetric
    {
        /// <summary>
        /// Gets the last modified date and time of the storage resource
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets the total count of files in the storage resource
        /// </summary>
        public long TotalFileCount { get; set; }

        /// <summary>
        /// Gets the total stream size of the storage resource
        /// </summary>
        public long TotalFileStreamSize { get; set; }

        /// <summary>
        /// Gets the total size of the storage resource
        /// </summary>
        public long TotalSize { get; set; }
    }
}


