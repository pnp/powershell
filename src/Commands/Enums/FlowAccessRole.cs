namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Possible permissions for accessing a Power Automate flow
    /// </summary>
    public enum FlowAccessRole
    {
        /// <summary>
        /// View only access level on the flow
        /// </summary>
        CanView,

        /// <summary>
        /// View with share access level on the flow
        /// </summary>
        CanViewWithShare,

        /// <summary>
        /// Edit access level on the flow
        /// </summary>
        CanEdit
    }
}
