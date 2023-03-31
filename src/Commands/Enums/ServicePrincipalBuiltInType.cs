namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Enum that defines the built in service principal types
    /// </summary>
    public enum ServicePrincipalBuiltInType
    {
        /// <summary>
        /// SharePoint Online => 00000003-0000-0ff1-ce00-000000000000
        /// </summary>
        SharePointOnline = 0,
        
        /// <summary>
        /// Microsoft Graph => 00000003-0000-0000-c000-000000000000
        /// </summary>
        MicrosoftGraph = 1
    }
}
