namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// Contains the properties of a checked out file
    /// </summary>
    public class CheckedOutFile
    {
        /// <summary>
        /// Server relative url to the checked out
        /// </summary>
        public string ServerRelativeUrl { get; set; }

        /// <summary>
        /// The user who has the file checked out
        /// </summary>
        public User CheckedOutBy { get; set; }
    }
}
