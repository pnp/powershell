namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Possible IdType values inside an oAuth JWT token
    /// </summary>
    public enum IdType : short
    {
        /// <summary>
        /// Unable to identify the token type
        /// </summary>
        Unknown,

        /// <summary>
        /// IdType user, indicates a delegate token
        /// </summary>
        Delegate,

        /// <summary>
        /// IdType app, indicates an application token
        /// </summary>
        Application
    }
}
