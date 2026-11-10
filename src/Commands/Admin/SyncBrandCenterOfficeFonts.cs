using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    /// <summary>
    /// Makes the Office font library offer the fonts of Brand Center (or of a local folder), with a
    /// font catalog built by PnP itself: no SharePoint Online Management Shell module, no Windows.
    ///
    /// File names are compared first. Nothing is downloaded or uploaded when the library is already
    /// in sync. Otherwise every font the library keeps plus the new ones is published again with a
    /// rebuilt catalog, replacing the previous upload as Set-SPOCustomFontCatalog does. -RemoveMissing
    /// also removes the Office fonts that are not in the source.
    /// </summary>
    [Cmdlet(VerbsData.Sync, "PnPBrandCenterOfficeFonts", SupportsShouldProcess = true)]
    public class SyncBrandCenterOfficeFonts : PnPSharePointOnlineAdminCmdlet
    {
        /// <summary>Where the fonts come from. Default: Brand Center.</summary>
        [Parameter(Mandatory = false)]
        public OfficeFontSource Source = OfficeFontSource.BrandCenter;

        /// <summary>Folder with the .ttf/.otf fonts, for -Source Local.</summary>
        [Parameter(Mandatory = false)]
        public string LocalFolder;

        /// <summary>Also remove the Office fonts that are not in the source.</summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveMissing;

        /// <summary>Publish without removing and adding the Office font library registration again.</summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter KeepRegistration;

        /// <summary>Publish without asking for confirmation.</summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        private static readonly StringComparer Names = StringComparer.OrdinalIgnoreCase;

        protected override void ExecuteCmdlet()
        {
            var registered = OfficeFontLibraryChecks.EnsureReady(Tenant, AdminContext, LogDebug, ThrowError);
            var officeLibrary = FontLibraryReader.EncodePath(registered?.LibraryUrl?.DecodedUrl);
            if (string.IsNullOrEmpty(officeLibrary))
            {
                ThrowError("The Office font library has no URL.", "NoOfficeFontLibraryConfigured");
            }
            var targetUrl = FontLibraryReader.ToAbsoluteUrl(BaseUri, officeLibrary);
            LogDebug($"Office font library: {targetUrl}");

            // 1. Names only, on both sides.
            using var source = OpenSource();
            using var office = FontLibraryReader.OpenLibraryWeb(AdminContext, BaseUri, officeLibrary, url => Connection.CloneContext(url), LogDebug);
            var officeEntries = FontLibraryReader.List(office, officeLibrary, CustomFontsCatalogPublisher.IsOfficeFont, LogDebug);

            if (source.Names.Count == 0)
            {
                WriteWarning($"No {string.Join(", ", CustomFontsCatalogPublisher.OfficeFontExtensions)} fonts found in {source.Description}.");
                return;
            }

            var plan = OfficeFontSyncPlan.Create(source.Names, officeEntries.Select(e => e.FileName), RemoveMissing);
            if (!plan.HasChanges)
            {
                WriteObject($"The Office font library is in sync with {source.Description} ({plan.Kept.Count} font(s))");
                return;
            }
            // -Force skips the confirmation, never -WhatIf.
            var whatIf = MyInvocation.BoundParameters.ContainsKey("WhatIf");
            if ((!Force || whatIf) && !ShouldProcess(targetUrl, $"Office fonts: {plan.Describe()}"))
            {
                return;
            }

            // 2. Download the new fonts, and the kept ones (the catalog is built from every font).
            var newFonts = source.Download(plan.Added.ToList());
            // The same file name can be in two folders of the library: publish it once.
            var keptFonts = FontLibraryReader.Download(office, officeEntries.Where(e => !plan.IsRemoved(e.FileName)).GroupBy(e => e.FileName, Names).Select(g => g.First()), LogDebug);
            var allFonts = keptFonts.Concat(newFonts).ToList();

            // 3. Publish every font with the catalog, the way Set-SPOCustomFontCatalog replaces the previous
            //    upload. Then delete removed font files the server may have left.
            //    Microsoft's remove/add of the registration around it makes Office reload the library.
            OfficeFontLibraryChecks.PublishAndRegister(Tenant, AdminContext, registered, targetUrl, KeepRegistration,
                () => CustomFontsCatalogPublisher.Publish(Tenant, AdminContext, office, allFonts, targetUrl, WriteWarning, LogDebug),
                WriteWarning, LogDebug);
            foreach (var entry in officeEntries.Where(e => plan.IsRemoved(e.FileName)))
            {
                try
                {
                    var file = office.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(entry.ServerRelativeUrl));
                    file.DeleteObject();
                    office.ExecuteQueryRetry();
                    LogDebug($"Deleted {entry.ServerRelativeUrl}");
                }
                catch (ServerException ex)
                {
                    LogDebug($"{entry.ServerRelativeUrl} was already gone ({ex.Message})");
                }
            }

            WriteWarning(OfficeFontLibraryChecks.PublicCatalogWarning);
            WriteObject($"Office font library: {plan.Added.Count} added, {plan.Removed.Count} removed, {keptFonts.Count + newFonts.Count} in total");
        }

        /// <summary>The fonts to publish: names, and a way to download only some of them.</summary>
        private sealed class FontSource : IDisposable
        {
            public string Description;
            public List<string> Names;
            public Func<IReadOnlyCollection<string>, List<CustomFontFile>> Download;
            public ClientContext Context;
            public void Dispose() => Context?.Dispose();
        }

        private FontSource OpenSource()
        {
            if (Source == OfficeFontSource.Local)
            {
                if (string.IsNullOrEmpty(LocalFolder))
                {
                    ThrowError("-LocalFolder is required with -Source Local.", "LocalFolderRequired", ErrorCategory.InvalidArgument);
                }
                var folder = Path.IsPathRooted(LocalFolder) ? LocalFolder : Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LocalFolder);
                if (!Directory.Exists(folder))
                {
                    ThrowError($"The folder '{folder}' does not exist.", "LocalFolderNotFound", ErrorCategory.ObjectNotFound);
                }
                var paths = Directory.EnumerateFiles(folder).Where(CustomFontsCatalogPublisher.IsOfficeFont).ToList();
                return new FontSource
                {
                    Description = folder,
                    Names = paths.Select(Path.GetFileName).ToList(),
                    Download = wanted => paths
                        .Where(p => wanted.Contains(Path.GetFileName(p), Names))
                        .Select(p => new CustomFontFile { FileName = Path.GetFileName(p), Content = System.IO.File.ReadAllBytes(p) })
                        .ToList(),
                };
            }

            var brandCenter = BrandCenterUtility.GetBrandCenterConfiguration(this, ClientContext);
            if (brandCenter == null || string.IsNullOrEmpty(brandCenter.SiteUrl))
            {
                ThrowError("Brand Center is not configured.", "NoBrandCenterConfigured");
            }
            var brandLibrary = FontLibraryReader.EncodePath(brandCenter.BrandFontLibraryUrl?.DecodedUrl);
            if (string.IsNullOrEmpty(brandLibrary))
            {
                ThrowError("No Brand Center font library is configured.", "NoBrandFontLibrary");
            }

            var ctx = FontLibraryReader.OpenLibraryWeb(AdminContext, BaseUri, brandLibrary, url => Connection.CloneContext(url), LogDebug);
            var entries = FontLibraryReader.List(ctx, brandLibrary, CustomFontsCatalogPublisher.IsOfficeFont, LogDebug);
            // Brand Center can hold the same file name in two folders; the first one wins.
            var byName = entries.GroupBy(e => e.FileName, Names).ToDictionary(g => g.Key, g => g.First(), Names);
            return new FontSource
            {
                Description = "Brand Center",
                Names = byName.Keys.ToList(),
                Context = ctx,
                Download = wanted => FontLibraryReader.Download(ctx, wanted.Select(n => byName[n]), LogDebug),
            };
        }

        private void ThrowError(string message, string errorId, ErrorCategory category = ErrorCategory.InvalidOperation) =>
            ThrowTerminatingError(new ErrorRecord(new PSInvalidOperationException(message), errorId, category, null));
    }
}
