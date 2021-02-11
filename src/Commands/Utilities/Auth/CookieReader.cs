using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PnP.PowerShell.Commands.Utilities.Auth
{
    internal static class CookieReader
    {
        /// <summary>
        /// Enables the retrieval of cookies that are marked as "HTTPOnly". 
        /// Do not use this flag if you expose a scriptable interface, 
        /// because this has security implications. It is imperative that 
        /// you use this flag only if you can guarantee that you will never 
        /// expose the cookie to third-party code by way of an 
        /// extensibility mechanism you provide. 
        /// Version:  Requires Internet Explorer 8.0 or later.
        /// </summary>
        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;

        /// <summary>
        /// Returns cookie contents as a string
        /// </summary>
        /// <param name="url">Url to get cookie</param>
        /// <returns>Returns Cookie contents as a string</returns>
        public static string GetCookie(string url)
        {

            int size = 512;
            StringBuilder sb = new StringBuilder(size);
            if (!NativeMethods.InternetGetCookieEx(url, null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
            {
                if (size < 0)
                {
                    return null;
                }
                sb = new StringBuilder(size);
                if (!NativeMethods.InternetGetCookieEx(url, null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
                {
                    return null;
                }
            }
            return sb.ToString();
        }

        public static void SetCookie(string url, string cookiename, string cookiedata)
        {
            NativeMethods.InternetSetCookieEx(url, cookiename, cookiedata, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero);
        }

        private static class NativeMethods
        {

            [DllImport("wininet.dll", EntryPoint = "InternetGetCookieEx", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool InternetGetCookieEx(
                string url,
                string cookieName,
                StringBuilder cookieData,
                ref int size,
                int flags,
                IntPtr pReserved);

            [DllImport("wininet.dll", EntryPoint = "InternetSetCookieEx", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool InternetSetCookieEx(
                string url,
                string cookieName,
                string cookieData,
                int flags,
                IntPtr pReserved);

        }
    }
}