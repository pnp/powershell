namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// See <a href="https://docs.microsoft.com/en-us/graph/api/resources/permission#roles-property-values">Graph Reference</a>
    /// </summary>
    public enum AzureADAppSitePermissionRole
    {
        Read,
        Write,
        Owner,

        // not listed in docs?
        Manage,
        FullControl
    }
}
