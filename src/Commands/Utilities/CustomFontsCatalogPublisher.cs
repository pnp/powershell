using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>A font file to publish to the Office font library.</summary>
    internal sealed class CustomFontFile
    {
        public string FileName { get; init; }
        public byte[] Content { get; init; }
    }

    /// <summary>
    /// Publishes custom fonts to the Office font library (OrgAssetType OfficeFontLibrary), on the PnP
    /// connection (access tokens work), with a catalog built by <see cref="OfficeFontCatalogBuilder"/>:
    /// no SharePoint Online Management Shell module, no Windows.
    ///
    ///   1. Tenant.GetCustomFontsMinorVersion(library)
    ///   2. OfficeFontCatalogBuilder builds &lt;major&gt;/fontcatalog*.fontconfig for catalog versions 1 and 2
    ///   3. Upload, with the same calls as Set-SPOCustomFontCatalog (16.0.27709):
    ///      up to 1 MB Tenant.UploadCustomFontsAndCatalog; over 1 MB the flight check,
    ///      Tenant.RemovePreviousCustomFontUpload, the files, then Tenant.IncrementCustomFontsMinorVersion
    ///
    /// See OfficeFontCatalog.md for the format and the upload details.
    /// </summary>
    internal static class CustomFontsCatalogPublisher
    {
        /// <summary>Font types Office desktop apps accept (web fonts such as WOFF are not).</summary>
        public static readonly string[] OfficeFontExtensions = [".ttf", ".otf"];

        public static bool IsOfficeFont(string fileName) =>
            OfficeFontExtensions.Contains(Path.GetExtension(fileName ?? string.Empty).ToLowerInvariant());

        /// <summary>Same threshold as Set-SPOCustomFontCatalog (CustomFonts.LargeUploadMinimum).</summary>
        public const int LargeUploadMinimum = 1000000;

        /// <summary>Builds the catalog of <paramref name="fonts"/> and publishes it with the font files.</summary>
        /// <param name="libraryContext">Context on the web that owns the library.</param>
        /// <param name="fonts">
        /// Every font the library offers afterwards. All of them are uploaded: like Set-SPOCustomFontCatalog,
        /// the previous upload is replaced, not merged.
        /// </param>
        public static void Publish(Tenant tenant, ClientContext adminContext, ClientContext libraryContext,
            IReadOnlyCollection<CustomFontFile> fonts, string libraryUrl, Action<string> warn, Action<string> verbose)
        {
            ValidateFonts(fonts);
            var minorVersion = GetMinorVersion(tenant, adminContext, libraryUrl, verbose);

            List<OfficeFontCatalogFile> catalog;
            try
            {
                catalog = OfficeFontCatalogBuilder.Build(fonts, minorVersion, warn, verbose);
            }
            catch (InvalidDataException ex)
            {
                throw new PSInvalidOperationException($"Could not build the Office font catalog: {ex.Message}");
            }

            var totalBytes = fonts.Sum(f => (long)f.Content.Length) + catalog.Sum(c => (long)c.Content.Length);
            if (totalBytes <= LargeUploadMinimum)
            {
                UploadSmall(tenant, adminContext, fonts, catalog, libraryUrl, verbose);
            }
            else
            {
                UploadLarge(tenant, adminContext, libraryContext, fonts, catalog, libraryUrl, verbose);
            }
        }

        /// <summary>
        /// Under 1 MB Set-SPOCustomFontCatalog sends everything in one call, and SharePoint replaces the
        /// previous upload, writes the files and bumps the minor version itself.
        /// </summary>
        private static void UploadSmall(Tenant tenant, ClientContext adminContext, IReadOnlyCollection<CustomFontFile> fonts,
            IReadOnlyCollection<OfficeFontCatalogFile> catalog, string libraryUrl, Action<string> verbose)
        {
            var resources = fonts
                .Select(f => new CustomFontsResource { fileName = Path.GetFileName(f.FileName), fullPath = f.FileName, byteArray = f.Content, type = CustomFontsResourceType.Font, MajVer = 0 })
                .Concat(catalog.Select(c => new CustomFontsResource { fileName = c.FileName, fullPath = $"{c.MajorVersion}/{c.FileName}", byteArray = c.Content, type = CustomFontsResourceType.Catalog, MajVer = c.MajorVersion }))
                .ToList();
            verbose($"Uploading {fonts.Count} font(s) and {catalog.Count} catalog file(s) with Tenant.UploadCustomFontsAndCatalog");
            var result = tenant.UploadCustomFontsAndCatalog(resources, libraryUrl);
            ExecuteFontCall(adminContext, libraryUrl);
            if (!result.Value)
            {
                throw new PSInvalidOperationException($"SharePoint did not accept the custom font upload to {libraryUrl} (UploadCustomFontsAndCatalog returned false).");
            }
        }

        private static void ValidateFonts(IReadOnlyCollection<CustomFontFile> fonts)
        {
            if (fonts == null || fonts.Count == 0)
            {
                throw new PSArgumentException("No font files to publish.");
            }
            var notOffice = fonts.Where(f => !IsOfficeFont(f.FileName)).Select(f => f.FileName).ToList();
            if (notOffice.Count > 0)
            {
                throw new PSArgumentException($"Only {string.Join(", ", OfficeFontExtensions)} fonts can be published to Office: {string.Join(", ", notOffice)}");
            }
        }

        /// <summary>
        /// Same sequence as Set-SPOCustomFontCatalog (16.0.27709) over 1 MB:
        /// IsOfficeFontLibraryOrgAssetFlightEnabled, RemovePreviousCustomFontUpload(catalog versions),
        /// fonts to the library root folder, catalog files to the folder of their version (1, 2),
        /// LogCustomFontsLargeUpload, IncrementCustomFontsMinorVersion.
        /// </summary>
        /// <param name="libraryContext">Context on the web that owns the library.</param>
        private static void UploadLarge(Tenant tenant, ClientContext adminContext, ClientContext libraryContext, IReadOnlyCollection<CustomFontFile> fonts,
            IReadOnlyCollection<OfficeFontCatalogFile> catalog, string libraryUrl, Action<string> verbose)
        {
            // Server-side gate; it throws when the Office font library feature is off for the tenant.
            tenant.IsOfficeFontLibraryOrgAssetFlightEnabled(OrgAssetType.OfficeFontLibrary);
            adminContext.ExecuteQueryRetry();

            var list = libraryContext.Web.GetListUsingPath(ResourcePath.FromDecodedUrl(FontLibraryReader.ToServerRelativePath(libraryUrl)));
            libraryContext.Load(list.RootFolder, f => f.ServerRelativeUrl);
            libraryContext.ExecuteQueryRetry();

            // Catalog versions only ("1", "2"), never font names: see OfficeFontCatalog.md.
            var majorVersions = catalog.Select(c => c.MajorVersion.ToString()).Distinct().ToList();
            verbose($"Removing the previous custom font upload (catalog versions {string.Join(", ", majorVersions)})");
            tenant.RemovePreviousCustomFontUpload(majorVersions, libraryUrl);
            ExecuteFontCall(adminContext, libraryUrl);

            // Folders are read only now: the call above may have removed the version folders.
            libraryContext.Load(list.RootFolder.Folders, all => all.Include(s => s.Name));
            libraryContext.ExecuteQueryRetry();

            foreach (var font in fonts)
            {
                verbose($"Uploading font {font.FileName}");
                AddFile(libraryContext, list.RootFolder, Path.GetFileName(font.FileName), font.Content);
            }
            // Snapshot of the existing folders: Folders.Add puts a new, unloaded Folder in the same
            // collection, and reading its Name would throw "has not been initialized".
            var existingFolders = list.RootFolder.Folders.ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var group in catalog.GroupBy(c => c.MajorVersion.ToString()))
            {
                var folder = existingFolders.TryGetValue(group.Key, out var existing)
                    ? existing
                    : list.RootFolder.Folders.Add(group.Key);
                foreach (var file in group)
                {
                    verbose($"Uploading catalog {group.Key}/{file.FileName}");
                    AddFile(libraryContext, folder, file.FileName, file.Content);
                }
            }

            try
            {
                // Usage log only; a failure must not fail the publish.
                tenant.LogCustomFontsLargeUpload(catalog.Count, fonts.Count, fonts.Count + catalog.Count);
                adminContext.ExecuteQueryRetry();
            }
            catch (ServerException ex)
            {
                verbose($"Could not log the upload: {ex.Message}");
            }
            // Office clients reload the catalog when the minor version changes.
            tenant.IncrementCustomFontsMinorVersion(libraryUrl);
            adminContext.ExecuteQueryRetry();
            verbose($"Uploaded {fonts.Count} font(s) and {catalog.Count} catalog file(s) to {libraryUrl}");
        }

        /// <summary>
        /// Runs a call that clears or replaces the previous upload. On a library whose previous upload was
        /// cleared with font names instead of catalog versions, SharePoint fails these calls with "urlOfFile"
        /// every time; Set-SPOCustomFontCatalog fails the same way, and no client call repairs it.
        /// </summary>
        private static void ExecuteFontCall(ClientContext adminContext, string libraryUrl)
        {
            try
            {
                adminContext.ExecuteQueryRetry();
            }
            catch (ServerException ex) when (ex.Message.Contains("urlOfFile", StringComparison.OrdinalIgnoreCase))
            {
                throw new PSInvalidOperationException(
                    $"SharePoint cannot replace the previous custom font upload of {libraryUrl}: its upload state is broken " +
                    $"(\"{ex.Message}\"). Set-SPOCustomFontCatalog fails the same way on this library. Register a new Office " +
                    "font library and publish to it, or ask Microsoft support to reset the custom fonts of this one.", ex);
            }
        }

        private static void AddFile(ClientContext context, Folder folder, string name, byte[] content)
        {
            using var stream = new MemoryStream(content);
            var file = folder.Files.Add(new FileCreationInformation { Url = name, ContentStream = stream, Overwrite = true });
            context.Load(file, f => f.Name);
            context.ExecuteQueryRetry();
        }

        /// <summary>Minor version the catalog is built with; Set-SPOCustomFontCatalog uses at least 1.</summary>
        private static uint GetMinorVersion(Tenant tenant, ClientContext adminContext, string libraryUrl, Action<string> verbose)
        {
            var minorResult = tenant.GetCustomFontsMinorVersion(libraryUrl);
            adminContext.ExecuteQueryRetry();
            var minorVersion = (uint)Math.Max(1, minorResult.Value);
            verbose($"Custom fonts minor version of {libraryUrl}: {minorVersion}");
            return minorVersion;
        }
    }
}
