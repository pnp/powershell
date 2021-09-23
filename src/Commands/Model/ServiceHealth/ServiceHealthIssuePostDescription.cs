namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// Represents the description of a service health issue post
    /// </summary>
    public class ServiceHealthIssuePostDescription
    {
        /// <summary>
        /// Format the <see cref="Content"/> is in, i.e. html
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The content of the service health issue post description
        /// </summary>
        public string Content { get; set; }
    }
}