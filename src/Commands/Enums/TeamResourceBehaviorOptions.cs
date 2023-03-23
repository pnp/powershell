using System;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// All allowed options for providing ResourceBehaviorOptions when creating a Microsoft Teams team
    /// </summary>
    /// <remarks>Documentation: https://learn.microsoft.com/graph/group-set-options#configure-groups</remarks>
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
        WelcomeEmailDisabled,

        /// <summary>
        /// Members are not subscribed to the group's calendar events in Outlook.
        /// </summary>
        SubscribeMembersToCalendarEventsDisabled,

        /// <summary>
        /// Changes made to the group in Exchange Online are not synced back to on-premises Active Directory.
        /// </summary>
        ConnectorsDisabled,

        /// <summary>
        /// Members can view the group calendar in Outlook but cannot make changes.
        /// </summary>
        CalendarMemberReadOnly
    }
}
