namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the modes that can be set on a Document Library for how to open documents
    /// </summary>
    public enum DocumentLibraryOpenDocumentsInMode : short
    {
        /// <summary>
        /// Opens the document in the client application, i.e. Word, Excel, PowerPoint, etc. on the local desktop
        /// </summary>
        InClientApplication = 0,

        /// <summary>
        /// Opens the document in the browser
        /// </summary>
        InBrowser = 1
    }
}
