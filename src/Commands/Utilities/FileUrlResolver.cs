using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.Framework.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Resolves a file URL as provided by the user to the server relative URL of the file it points at. 
    /// A file name can hold a sequence such as %20 literally, 
    /// so the URL is used as provided when a file exists there and only decoded when it does not.
    /// </summary>
    public static class FileUrlResolver
    {
        /// <summary>
        /// Resolves the URL using CSOM to check if a file exists at the literal URL. 
        /// Pass null as the web URL to leave a web relative URL as is.
        /// </summary>
        public static string Resolve(string url, string webServerRelativeUrl, ClientContext clientContext, Web web)
        {
            return Resolve(url, webServerRelativeUrl, candidate =>
            {
                var file = web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(candidate));
                clientContext.Load(file, f => f.Exists);
                clientContext.ExecuteQueryRetry();
                return file.Exists;
            });
        }

        /// <summary>
        /// Resolves the URL using PnP Core to check if a file exists at the literal URL. 
        /// Pass null as the web URL to leave a web relative URL as is.
        /// </summary>
        public static string Resolve(string url, string webServerRelativeUrl, PnPContext pnpContext)
        {
            return Resolve(url, webServerRelativeUrl, candidate => pnpContext.Web.GetFileByServerRelativeUrlOrDefault(candidate) != null);
        }

        private static string Resolve(string url, string webServerRelativeUrl, Func<string, bool> fileExists)
        {
            var literalUrl = ToServerRelativeUrl(url, webServerRelativeUrl);

            // The + character is excluded from decoding as it would otherwise turn into a space.
            var decodedUrl = ToServerRelativeUrl(UrlUtilities.UrlDecode(url.Replace("+", "%2B")), webServerRelativeUrl);

            // Nothing decoded means there are no two candidates to pick between, so no lookup is needed.
            if (decodedUrl.Equals(literalUrl, StringComparison.Ordinal))
            {
                return literalUrl;
            }

            try
            {
                return fileExists(literalUrl) ? literalUrl : decodedUrl;
            }
            catch (Exception e) when (e is not PipelineStoppedException)
            {
                // The lookup only picks between two candidates, so it must never fail the cmdlet itself.
                return decodedUrl;
            }
        }

        private static string ToServerRelativeUrl(string url, string webServerRelativeUrl)
        {
            return string.IsNullOrEmpty(webServerRelativeUrl) || url.StartsWith(webServerRelativeUrl, StringComparison.OrdinalIgnoreCase)
                ? url
                : UrlUtility.Combine(webServerRelativeUrl, url);
        }
    }
}
