namespace PnP.PowerShell.Commands.Enums
{
    public enum AlertFilter
    {
        AnythingChanges = 0,
        SomeoneElseChangesAnItem = 1,
        SomeoneElseChangesItemCreatedByMe = 2,
        SomeoneElseChangesItemLastModifiedByMe = 3
    }
}
