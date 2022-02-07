namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// Represents ViewPoint inside a service update message
    /// </summary>
    public class ServiceUpdateMessageViewPoint
    {
        /// <summary>
        /// Indicates if this service announcement has been read
        /// </summary>
        public bool? IsRead { get; set; }

        /// <summary>
        /// Indicates if this service announcement has been archived
        /// </summary>
        public bool? IsArchived { get; set; }

        /// <summary>
        /// Indicates if this service announcement has been favored
        /// </summary>
        public bool? IsFavorited { get; set; }
    }
}