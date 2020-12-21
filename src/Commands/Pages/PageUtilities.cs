namespace PnP.PowerShell.Commands.Pages
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