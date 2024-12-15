using System.Runtime.InteropServices;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static string GetOSString()
        {
            if (IsWindows())
            {
                return "Windows";
            }
            else if (IsMacOS())
            {
                return "MacOS";
            }
            else if (IsLinux())
            {
                return "Linux";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}
