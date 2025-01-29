namespace PnP.PowerShell.Commands.Model.EnterpriseAppInsights
{
    /// <summary>
    /// The states an Enterprise App Insights report can be in
    /// </summary>
    public enum ReportStatus : short
    {
        NotStarted,
        InProgress,
        InQueue,
        Completed,
        ToBeDeleted,
        NotFound,
        Failed,
        Archived
    }
}