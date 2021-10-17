using System;
using System.Collections.Generic;
using System.Text;

namespace PnP.PowerShell.Commands.Enums
{
    [Flags()]
    public enum TeamResourceBehaviorOptions
    {
        AllowOnlyMembersToPost,
        HideGroupInOutlook,
        SubscribeNewGroupMembers,
        WelcomeEmailEnabled
    }
}
