using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.Framework.Utilities;
using System;
using System.Linq.Expressions;
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
        /// Returns the PnP Core file the URL points at. PnP Core turns a %20 in a URL back into a space, so a file
        /// whose name holds one literally is addressed by its unique id, which CSOM looks up from the literal URL.
        /// Pass null as the web URL to leave a web relative URL as is, or a null context to skip the lookup.
        /// </summary>
        public static IFile ResolveFile(string url, string webServerRelativeUrl, ClientContext clientContext, Web web, PnPContext pnpContext, params Expression<Func<IFile, object>>[] expressions)
        {
            var literalUrl = ToServerRelativeUrl(url, webServerRelativeUrl);

            // The + character is excluded from decoding as it would otherwise turn into a space.
            var decodedUrl = ToServerRelativeUrl(UrlUtilities.UrlDecode(url.Replace("+", "%2B")), webServerRelativeUrl);

            // Nothing decoded means there is nothing PnP Core would rewrite, so the file can be addressed by its URL.
            var uniqueId = Guid.Empty;
            if (!decodedUrl.Equals(literalUrl, StringComparison.Ordinal) && clientContext != null && web != null)
            {
                try
                {
                    var literalFile = web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(literalUrl));
                    clientContext.Load(literalFile, f => f.Exists, f => f.UniqueId);
                    clientContext.ExecuteQueryRetry();

                    if (literalFile.Exists)
                    {
                        uniqueId = literalFile.UniqueId;
                    }
                }
                catch (Exception e) when (e is not PipelineStoppedException)
                {
                    // The lookup only picks between two candidates, so it must never fail the cmdlet itself. Retrieving
                    // the file itself is deliberately left outside this catch so that its errors reach the caller.
                }
            }

            return uniqueId == Guid.Empty
                ? pnpContext.Web.GetFileByServerRelativeUrl(decodedUrl, expressions)
                : pnpContext.Web.GetFileById(uniqueId, expressions);
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
