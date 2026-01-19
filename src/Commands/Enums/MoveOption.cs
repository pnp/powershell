namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Represents options for move operations
    /// </summary>
    [System.Flags]
    public enum MoveOption
    {
        None = 0,
        ValidationOnly = 1,
        SuppressAllWarning = 2,
        SuppressMarketplaceAppCheck = 4,
        SuppressWorkflow2013Check = 8,
        SuppressBcsCheck = 16
    }
}