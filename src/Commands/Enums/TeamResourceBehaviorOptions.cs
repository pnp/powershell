using System;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// All allowed options for providing ResourceBehaviorOptions when creating a Microsoft Teams team
    /// </summary>
    [Flags()]
    public enum TeamResourceBehaviorOptions
    {
        /// <summary>
        /// Only members will be allowed to post messages
        /// </summary>
        AllowOnlyMembersToPost,

        /// <summary>
        /// Hides the Microsoft 365 Group in the Global Address List
        /// </summary>
        HideGroupInOutlook,

        /// <summary>
        /// Members added to the Team will follow the Team by default
        /// </summary>
        SubscribeNewGroupMembers,

        /// <summary>
        /// Do not send out the out of the box welcome e-mail to members getting added to the Microsoft Teams team
        /// </summary>
        WelcomeEmailDisabled
    }
}
