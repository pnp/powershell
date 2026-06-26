namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the status values supported by Microsoft Graph for To Do tasks.
    /// </summary>
    public enum ToDoTaskStatus
    {
        /// <summary>
        /// Indicates the task has not been started.
        /// </summary>
        NotStarted,

        /// <summary>
        /// Indicates the task is in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// Indicates the task has been completed.
        /// </summary>
        Completed,

        /// <summary>
        /// Indicates the task is waiting on others.
        /// </summary>
        WaitingOnOthers,

        /// <summary>
        /// Indicates the task has been deferred.
        /// </summary>
        Deferred
    }
}
