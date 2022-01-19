using System;
using System.Linq;
#if !NETFRAMEWORK
using System.Web;
#endif

namespace PnP.PowerShell.Commands.Utilities
{
    public static class UrlUtilities
    {
        public static string GetTenantAdministrationUrl(Uri uri)
        {
            var uriParts = uri.Host.Split('.');
            if (uriParts[0].EndsWith("-admin")) return uri.OriginalString;
            if (uriParts[0].EndsWith("-my")) return $"https://{uriParts[0].Remove(uriParts[0].Length - 3, 3)}-admin.{string.Join(".", uriParts.Skip(1))}";
            if (!uriParts[0].EndsWith("-admin")) return $"https://{uriParts[0]}-admin.{string.Join(".", uriParts.Skip(1))}";
            return null;
        }

        public static string GetTenantAdministrationUrl(string url)
        {
            return GetTenantAdministrationUrl(new Uri(url));
        }

        public static bool IsTenantAdministrationUrl(Uri uri)
        {
            var uriParts = uri.Host.Split('.');
            return uriParts[0].EndsWith("-admin");
        }

        public static bool IsTenantAdministrationUrl(string url)
        {
            return IsTenantAdministrationUrl(new Uri(url));
        }

        public static string UrlEncode(string urlToEncode)
        {
#if NETFRAMEWORK
            return System.Net.WebUtility.UrlEncode(urlToEncode);
#else
            return HttpUtility.UrlEncode(urlToEncode);
#endif
        }

        public static string UrlDecode(string urlToEncode)
        {
#if NETFRAMEWORK
            return System.Net.WebUtility.UrlDecode(urlToEncode);
#else
            return HttpUtility.UrlDecode(urlToEncode);
#endif
        }        

        public static bool IsPersonalSiteUrl(string url)
        {
            Uri uri = new Uri(url);
            if (IsMySite(uri))
            {
                if (!string.IsNullOrWhiteSpace(uri.AbsolutePath))
                {
                    return uri.AbsolutePath.StartsWith("/personal/", StringComparison.OrdinalIgnoreCase);
                }
                return false;
            }
            return false;
        }

        public static bool IsMySite(Uri uri)
        {
            ValidateUri("path", uri);
            return uri.Host.IndexOf("-my.", StringComparison.OrdinalIgnoreCase) > 0;
        }

        public static void ValidateUri(string name, Uri uri)
        {
            if (string.IsNullOrEmpty(name) || uri == null || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException(name);
            }
        }

    }
}