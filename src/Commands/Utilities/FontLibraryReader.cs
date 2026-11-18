using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>A file in a font library: its name and server-relative URL.</summary>
    internal sealed class FontLibraryEntry
    {
        public string FileName { get; init; }
        public string ServerRelativeUrl { get; init; }
    }

    /// <summary>Reads font files from a SharePoint library (Brand Center fonts or the Office fonts library).</summary>
    internal static class FontLibraryReader
    {
        /// <summary>
        /// Decoded server-relative path ("/sites/brand/Office Fonts") from a library URL that can be
        /// absolute, server-relative or relative, and URL-encoded or not.
        /// </summary>
        public static string ToServerRelativePath(string libraryUrl)
        {
            if (string.IsNullOrWhiteSpace(libraryUrl)) return null;
            var path = Uri.TryCreate(libraryUrl, UriKind.Absolute, out var absolute) && absolute.Scheme.StartsWith("http")
                ? absolute.AbsolutePath
                : libraryUrl;
            return "/" + Uri.UnescapeDataString(path).Trim('/');
        }

        /// <summary>
        /// URL-encodes each path segment of an already decoded URL (a ResourcePath.DecodedUrl), so that
        /// <see cref="ToServerRelativePath"/> decodes it exactly once, even when the path contains '%'.
        /// </summary>
        public static string EncodePath(string decodedUrl)
        {
            if (string.IsNullOrEmpty(decodedUrl)) return decodedUrl;
            var prefix = string.Empty;
            var path = decodedUrl;
            var scheme = decodedUrl.IndexOf("://", StringComparison.Ordinal);
            if (scheme > 0)
            {
                var slash = decodedUrl.IndexOf('/', scheme + 3);
                prefix = slash < 0 ? decodedUrl : decodedUrl[..slash];
                path = slash < 0 ? string.Empty : decodedUrl[slash..];
            }
            return prefix + string.Join("/", path.Split('/').Select(Uri.EscapeDataString));
        }

        /// <summary>Absolute URL of a library on the tenant.</summary>
        public static string ToAbsoluteUrl(Uri tenantRoot, string libraryUrl) =>
            string.IsNullOrEmpty(libraryUrl) ? null : tenantRoot.ToString().TrimEnd('/') + ToServerRelativePath(libraryUrl);

        /// <summary>
        /// Opens a context on the web that owns the library. SharePoint is asked which web that is, so
        /// paths always start with that web's ServerRelativeUrl, whatever site the library is on.
        /// </summary>
        public static ClientContext OpenLibraryWeb(ClientContext adminContext, Uri tenantRoot, string libraryUrl,
            Func<string, ClientContext> cloneContext, Action<string> verbose)
        {
            var serverRelative = ToServerRelativePath(libraryUrl);
            var webUrl = Web.WebUrlFromFolderUrlDirect(adminContext, new Uri(tenantRoot, serverRelative));
            verbose($"Library {serverRelative} is on {webUrl}");
            return cloneContext(webUrl.ToString());
        }

        /// <summary>
        /// Lists the files in the library, in every folder, whose name passes <paramref name="include"/>.
        /// Names only: nothing is downloaded.
        /// </summary>
        public static List<FontLibraryEntry> List(ClientContext webContext, string libraryUrl, Func<string, bool> include, Action<string> verbose)
        {
            var list = webContext.Web.GetList(ToServerRelativePath(libraryUrl));
            // Page through every item and keep files in memory: a Where on FSObjType is not indexed and
            // would hit the list view threshold on large libraries.
            var rows = new List<ListItem>();
            ListItemCollectionPosition position = null;
            do
            {
                var query = new CamlQuery
                {
                    ViewXml = "<View Scope='RecursiveAll'><RowLimit Paged='TRUE'>2000</RowLimit></View>",
                    ListItemCollectionPosition = position,
                };
                var items = list.GetItems(query);
                webContext.Load(items, all => all.ListItemCollectionPosition, all => all.Include(i => i["FileLeafRef"], i => i["FileRef"], i => i["FSObjType"]));
                webContext.ExecuteQueryRetry();
                rows.AddRange(items);
                position = items.ListItemCollectionPosition;
            } while (position != null);

            var entries = rows
                .Where(i => i["FSObjType"]?.ToString() == "0")
                .Select(i => new FontLibraryEntry { FileName = i["FileLeafRef"]?.ToString(), ServerRelativeUrl = i["FileRef"]?.ToString() })
                .Where(e => !string.IsNullOrEmpty(e.FileName) && include(e.FileName))
                .ToList();
            verbose($"{libraryUrl}: {entries.Count} font(s) {string.Join(", ", entries.Select(e => e.FileName))}");
            return entries;
        }

        /// <summary>Downloads the given files.</summary>
        public static List<CustomFontFile> Download(ClientContext webContext, IEnumerable<FontLibraryEntry> entries, Action<string> verbose)
        {
            var fonts = new List<CustomFontFile>();
            foreach (var entry in entries)
            {
                verbose($"Downloading: {entry.ServerRelativeUrl}");
                var stream = webContext.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(entry.ServerRelativeUrl)).OpenBinaryStream();
                webContext.ExecuteQueryRetry();

                using var ms = new MemoryStream();
                stream.Value.CopyTo(ms);
                fonts.Add(new CustomFontFile { FileName = entry.FileName, Content = ms.ToArray() });
            }
            return fonts;
        }
    }
}
