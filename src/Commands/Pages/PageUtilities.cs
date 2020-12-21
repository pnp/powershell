namespace PnP.PowerShell.Commands.ClientSidePages
{
    internal static class PageUtilities
    {
        public static string EnsureCorrectPageName(string pageName)
        {
            if (pageName != null && !pageName.EndsWith(".aspx"))
                pageName += ".aspx";

            return pageName;
        }
    }
}