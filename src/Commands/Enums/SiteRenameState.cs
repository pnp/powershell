namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// State of a site rename job, as SharePoint Online reports it in the SiteRenameJobs REST API.
    /// </summary>
    internal enum SiteRenameState
    {
        Queued = 0,
        InProgress = 1,
        Success = 2,
        Failed = 3,
        Suspended = 4,
        Canceling = 5,
        Canceled = 6
    }
}
