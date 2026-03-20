namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Represents the state of a user move operation
    /// </summary>
    public enum MoveState
    {
        All,
        NotStarted,
        InProgress,
        Success,
        Failed,
        ReadyToTrigger,
        MovedByOtherMeans
    }
}