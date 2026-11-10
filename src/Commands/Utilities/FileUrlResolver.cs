using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.Framework.Utilities;
using System;
using System.Linq.Expressions;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Resolves a file URL as provided by the user to the file it points at. A file name can hold a sequence such as
    /// %20 literally, so the URL is used as provided when a file exists there and only decoded when it does not.
    /// </summary>
    public static class FileUrlResolver
    {
        /// <summary>
        /// The server error code SharePoint reports when there is no file at the requested path.
        /// </summary>
        private const int FileNotFoundServerErrorCode = -2147024894;

        /// <summary>
        /// Resolves the URL to the server relative URL of the file it points at, for use with the CSOM APIs.
        /// Pass null as the web URL to leave a web relative URL as is.
        /// </summary>
        public static string Resolve(string url, string webServerRelativeUrl, ClientContext clientContext, Web web)
        {
            return ResolveCore(url, webServerRelativeUrl, clientContext, web).ServerRelativeUrl;
        }

        /// <summary>
        /// Resolves the URL to the PnP Core file it points at. PnP Core replaces a %20 in a URL with a space before it
        /// calls SharePoint, so a path still holding one is addressed by its unique id instead.
        /// Pass null as the web URL to leave a web relative URL as is, or a null context to skip the lookup.
        /// </summary>
        public static IFile ResolveFile(string url, string webServerRelativeUrl, ClientContext clientContext, Web web, PnPContext pnpContext, params Expression<Func<IFile, object>>[] expressions)
        {
            var (serverRelativeUrl, uniqueId) = ResolveCore(url, webServerRelativeUrl, clientContext, web);

            // Decoding once can still leave a %20 behind, as the browser URL of a file whose name holds one literally
            // does. Look that file up as well so it too is addressed by its unique id rather than by its URL.
            if (uniqueId == Guid.Empty && clientContext != null && web != null && serverRelativeUrl.Contains("%20", StringComparison.Ordinal))
            {
                uniqueId = TryGetFileId(serverRelativeUrl, clientContext, web);
            }

            return uniqueId == Guid.Empty
                ? pnpContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl, expressions)
                : pnpContext.Web.GetFileById(uniqueId, expressions);
        }

        /// <summary>
        /// Picks between the URL as provided and its decoded form, returning the unique id of the file as well when the
        /// lookup needed to make that choice has already established it.
        /// </summary>
        private static (string ServerRelativeUrl, Guid UniqueId) ResolveCore(string url, string webServerRelativeUrl, ClientContext clientContext, Web web)
        {
            var literalUrl = ToServerRelativeUrl(url, webServerRelativeUrl);

            // The + character is excluded from decoding as it would otherwise turn into a space.
            var decodedUrl = ToServerRelativeUrl(UrlUtilities.UrlDecode(url.Replace("+", "%2B")), webServerRelativeUrl);

            // Nothing decoded means there are no two candidates to pick between, so no lookup is needed.
            if (decodedUrl.Equals(literalUrl, StringComparison.Ordinal))
            {
                return (literalUrl, Guid.Empty);
            }

            // Without a SharePoint context there is nothing to pick with, so keep the decoded form.
            if (clientContext == null || web == null)
            {
                return (decodedUrl, Guid.Empty);
            }

            var uniqueId = TryGetFileId(literalUrl, clientContext, web);

            return uniqueId == Guid.Empty ? (decodedUrl, Guid.Empty) : (literalUrl, uniqueId);
        }

        /// <summary>
        /// Returns the unique id of the file at the given URL, or an empty Guid when SharePoint reports there is no file
        /// there. Any other failure is left to surface: reading it as an absent file would let a denied or otherwise
        /// failed lookup silently redirect the caller at a differently named sibling.
        /// </summary>
        private static Guid TryGetFileId(string serverRelativeUrl, ClientContext clientContext, Web web)
        {
            var file = web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            clientContext.Load(file, f => f.Exists, f => f.UniqueId);

            try
            {
                clientContext.ExecuteQueryRetry();
            }
            catch (ServerException e) when (e.ServerErrorCode == FileNotFoundServerErrorCode)
            {
                return Guid.Empty;
            }

            return file.Exists ? file.UniqueId : Guid.Empty;
        }

        private static string ToServerRelativeUrl(string url, string webServerRelativeUrl)
        {
            return string.IsNullOrEmpty(webServerRelativeUrl) || url.StartsWith(webServerRelativeUrl, StringComparison.OrdinalIgnoreCase)
                ? url
                : UrlUtility.Combine(webServerRelativeUrl, url);
        }
    }
}
