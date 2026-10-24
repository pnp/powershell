using System;
using System.IO;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class FileUtilities
    {
        internal static bool IsOpenOfficeFile(Stream stream)
        {
            byte[] bytes = new byte[6];
            if (stream.ReadAtLeast(bytes, bytes.Length, throwOnEndOfStream: false) != bytes.Length)
            {
                return false;
            }
            var signature = Convert.ToHexString(bytes);
            // SIG 50 4B 03 04 14 00
            return signature == "504B03041400";
        }
    }
}
