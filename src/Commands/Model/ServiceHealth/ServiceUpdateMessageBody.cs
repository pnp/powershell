namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// Represents a message inside a service update
    /// </summary>
    public class ServiceUpdateMessageBody
    {
        /// <summary>
        /// Format the <see cref="Content"/> is in, i.e. html
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The content of the service update message
        /// </summary>
        public string Content { get; set; }
    }
}