namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Scopes to which Access level for the user on the flow. 
    /// </summary>
    public enum FlowUserRoleName
    {
        /// <summary>
        /// sets the 'view' access level on the flow for the user/group
        /// </summary>
        CanView,

        /// <summary>
        /// sets the 'view with share' access level on the flow for the user/group
        /// </summary>
        CanViewWithShare,

        /// <summary>
        /// sets the 'edit' access level on the flow for the user/group
        /// </summary>
        CanEdit,
    }
}
