namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Types of errors that can occur while performing a SharePoint Online User Profile Import
    /// </summary>
    public enum SharePointUserProfileImportProfilePropertiesJobError
    {
        NoError = 0,
        InternalError = 1,
        DataFileNotExist = 20,
        DataFileNotInTenant = 21,
        DataFileTooBig = 22,
        InvalidDataFile = 23,
        ImportCompleteWithError = 30
    }
}