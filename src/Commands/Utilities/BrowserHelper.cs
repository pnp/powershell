using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class BrowserHelper
    {
        public static void LaunchBrowser(string url)
        {
            if (OperatingSystem.IsWindows())
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); // Works ok on windows
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start("xdg-open", url);  // Works ok on linux
            }
            else if (OperatingSystem.IsMacOS())
            {
                Process.Start("open", url); // Not tested
            }
        }
    }
}
