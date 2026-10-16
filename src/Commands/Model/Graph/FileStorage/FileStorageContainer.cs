using System;

namespace PnP.PowerShell.Commands.Model.Graph.FileStorage
{
    /// <summary>
    /// A SharePoint Embedded container, as returned by Microsoft Graph
    /// See <a href="https://learn.microsoft.com/graph/api/resources/filestoragecontainer">Graph Reference</a>
    /// </summary>
    public class FileStorageContainer
    {
        /// <summary>
        /// Id of the container
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Display name of the container
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the container
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Id of the container type of the container
        /// </summary>
        public Guid? ContainerTypeId { get; set; }

        /// <summary>
        /// Status of the container, inactive or active. Inactive containers are deleted automatically 24 hours after they have been created.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date and time the container was created
        /// </summary>
        public DateTimeOffset? CreatedDateTime { get; set; }

        /// <summary>
        /// Settings of the container
        /// </summary>
        public FileStorageContainerSettings Settings { get; set; }
    }
}
