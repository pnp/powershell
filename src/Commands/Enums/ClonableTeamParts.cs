using System;
using System.Runtime.Serialization;

namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Describes which part of a team should be cloned.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/graph/api/resources/clonableteamparts?view=graph-rest-1.0">
    [Flags]
    public enum ClonableTeamParts
    {
        [EnumMember(Value = "apps")]
        Apps = 1,

        [EnumMember(Value = "tabs")]
        Tabs = 2,

        [EnumMember(Value = "settings")]
        Settings = 4,

        [EnumMember(Value = "channels")]
        Channels = 8,

        [EnumMember(Value = "members")]
        Members = 0x10
    }
}
