using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BrandCenter;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    /// <summary>
    /// Publishes the fonts in a folder to the Office font library, with a font catalog built by
    /// PnP itself. Same result as Set-SPOCustomFontCatalog, but on a PnP connection, and it needs
    /// neither the SharePoint Online Management Shell module nor Windows.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "PnPOfficeFontCatalog", SupportsShouldProcess = true)]
    public class SetOfficeFontCatalog : PnPSharePointOnlineAdminCmdlet
    {
        /// <summary>Folder with every .ttf/.otf font the Office font library should offer.</summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string FontFolder;

        /// <summary>URL of the Office font library. Default: the library registered as the Office font library.</summary>
        [Parameter(Mandatory = false)]
        public string LibraryUrl;

        /// <summary>
        /// Publish without removing and adding the library registration again. By default the cmdlet
        /// follows Microsoft's procedure for changing a font library: Remove-SPOOrgAssetsLibrary, upload,
        /// Add-SPOOrgAssetsLibrary.
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter KeepRegistration;

        protected override void ExecuteCmdlet()
        {
            var folder = Path.IsPathRooted(FontFolder) ? FontFolder : Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, FontFolder);
            if (!Directory.Exists(folder))
            {
                ThrowError($"The font folder '{folder}' does not exist.", "FontFolderNotFound", ErrorCategory.ObjectNotFound);
            }

            var allFiles = Directory.EnumerateFiles(folder).ToList();
            var fontPaths = allFiles.Where(CustomFontsCatalogPublisher.IsOfficeFont).ToList();
            foreach (var skipped in allFiles.Except(fontPaths))
            {
                WriteWarning($"Skipped '{Path.GetFileName(skipped)}': Office apps only use {string.Join(", ", CustomFontsCatalogPublisher.OfficeFontExtensions)} fonts.");
            }
            if (fontPaths.Count == 0)
            {
                ThrowError($"No {string.Join(", ", CustomFontsCatalogPublisher.OfficeFontExtensions)} fonts were found in '{folder}'.", "NoFontFilesFound", ErrorCategory.InvalidData);
            }

            var registered = OfficeFontLibraryChecks.EnsureReady(Tenant, AdminContext, LogDebug, ThrowError, requireLibrary: false);
            if (string.IsNullOrWhiteSpace(LibraryUrl))
            {
                if (registered == null)
                {
                    ThrowError("No Office font library is registered. Pass -LibraryUrl with the library to publish to.", "NoOfficeFontLibraryConfigured", ErrorCategory.InvalidArgument);
                }
                LibraryUrl = FontLibraryReader.ToAbsoluteUrl(BaseUri, FontLibraryReader.EncodePath(registered.LibraryUrl?.DecodedUrl));
            }
            // Office only reads the library registered as OfficeFontLibrary: publishing anywhere else
            // succeeds but no client ever sees the fonts.
            if (registered != null && !string.Equals(FontLibraryReader.ToServerRelativePath(FontLibraryReader.EncodePath(registered.LibraryUrl?.DecodedUrl)), FontLibraryReader.ToServerRelativePath(LibraryUrl), System.StringComparison.OrdinalIgnoreCase))
            {
                ThrowError($"'{LibraryUrl}' is not the Office font library. The library registered as OfficeFontLibrary is '{registered.LibraryUrl?.DecodedUrl}'.", "NotTheOfficeFontLibrary", ErrorCategory.InvalidArgument);
            }

            // The catalog lists only the fonts in this folder: it replaces what the library offered before.
            var fonts = fontPaths
                .Select(path => new CustomFontFile { FileName = Path.GetFileName(path), Content = System.IO.File.ReadAllBytes(path) })
                .ToList();
            // Set-SPOCustomFontCatalog passes the URL as typed (trimmed) to every Tenant call; do the same
            // for absolute URLs, so the minor version is read and bumped under the same key.
            var typed = LibraryUrl.Trim();
            var libraryUrl = System.Uri.TryCreate(typed, System.UriKind.Absolute, out var abs) && abs.Scheme.StartsWith("http")
                ? typed
                : FontLibraryReader.ToAbsoluteUrl(BaseUri, typed);
            var action = registered == null
                ? $"Publish {fonts.Count} font(s) and register the library as the Office font library"
                : KeepRegistration
                    ? $"Publish {fonts.Count} font(s)"
                    : $"Remove the Office font library registration, publish {fonts.Count} font(s), register the library again";
            if (!ShouldProcess(libraryUrl, action))
            {
                return;
            }
            using (var library = FontLibraryReader.OpenLibraryWeb(AdminContext, BaseUri, libraryUrl, url => Connection.CloneContext(url), LogDebug))
            {
                OfficeFontLibraryChecks.PublishAndRegister(Tenant, AdminContext, registered, libraryUrl, KeepRegistration,
                    () => CustomFontsCatalogPublisher.Publish(Tenant, AdminContext, library, fonts, libraryUrl, WriteWarning, LogDebug),
                    WriteWarning, LogDebug);
            }

            WriteWarning(OfficeFontLibraryChecks.PublicCatalogWarning);
            WriteObject($"Published {fonts.Count} font(s) to {libraryUrl}");
        }

        private void ThrowError(string message, string errorId, ErrorCategory category = ErrorCategory.InvalidOperation) =>
            ThrowTerminatingError(new ErrorRecord(new PSInvalidOperationException(message), errorId, category, null));
    }
}
