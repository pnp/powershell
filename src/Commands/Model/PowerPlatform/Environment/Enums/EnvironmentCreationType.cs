namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment.Enums
{
    /// <summary>
    /// Contains the possible creators of a PowerPlatform environment
    /// </summary>
    public enum EnvironmentCreationType
    {
        /// <summary>
        /// Environment is the default environment that comes with a tenant
        /// </summary>
        DefaultTenant,

        /// <summary>
        /// Environment has been created by a user
        /// </summary>
        User
    }
}