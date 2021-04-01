namespace PnP.PowerShell.Commands.Model.Syntex
{
    /// <summary>
    /// The result of a SharePoint Syntex model publication
    /// </summary>
    public class SyntexPublicationResult
    {
        /// <summary>
        /// The publication error (if there was any)
        /// </summary>
        public string ErrorMessage { get; internal set; }

        /// <summary>
        /// The published model
        /// </summary>
        public SyntexModelPublication Publication { get; internal set; }

        /// <summary>
        /// The status code of the publication, 2xx == success
        /// </summary>
        public int StatusCode { get; internal set; }
    }
}
