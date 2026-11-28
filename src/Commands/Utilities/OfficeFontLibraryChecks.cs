using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BrandCenter;
using Microsoft.SharePoint.Client;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>Checks shared by the Office font cmdlets.</summary>
    internal static class OfficeFontLibraryChecks
    {
        public const string PublicCatalogWarning =
            "Fonts and their catalog are stored on the public CDN and can be read by anyone who has their URLs, including people outside your organization. Do not publish fonts whose license forbids cloud hosting.";

        public const string PropagationWarning =
            "Office apps can take up to 24 hours to show the fonts after the Office font library is registered again.";

        /// <summary>
        /// Fails unless the public CDN is on and, with <paramref name="requireLibrary"/>, an Office font
        /// library is registered. Returns the registered library, or null.
        /// </summary>
        public static OrgAssetsLibrary EnsureReady(Tenant tenant, ClientContext adminContext, Action<string> verbose,
            Action<string, string, ErrorCategory> fail, bool requireLibrary = true)
        {
            verbose("Checking the Office font library and the public CDN");
            var orgAssets = tenant.GetOrgAssets();
            var cdnEnabled = tenant.GetTenantCdnEnabled(SPOTenantCdnType.Public);
            adminContext.ExecuteQueryRetry();

            var library = orgAssets.Value?.OrgAssetsLibraries?.FirstOrDefault(lib => lib.OrgAssetType == OrgAssetType.OfficeFontLibrary);
            if (library == null && requireLibrary)
            {
                fail("No Office font library is configured. Add an organization assets library with OrgAssetType OfficeFontLibrary first.", "NoOfficeFontLibraryConfigured", ErrorCategory.InvalidOperation);
            }
            if (!cdnEnabled.Value)
            {
                fail("The public CDN is not enabled for this tenant. Enable it before publishing fonts.", "PublicCdnNotEnabled", ErrorCategory.InvalidOperation);
            }
            return library;
        }

        /// <summary>
        /// Runs <paramref name="publish"/> inside Microsoft's procedure for changing a font library:
        /// remove the OfficeFontLibrary registration, upload, add the registration again. The upload alone
        /// replaces the catalog in SharePoint, but Microsoft documents that Office only picks up a changed
        /// library once it is registered again.
        ///
        /// A library that is not registered yet is registered after the upload. With
        /// <paramref name="keepRegistration"/> an existing registration is left alone. The registration is
        /// added again even when the upload fails, so a failed publish never leaves the tenant without its
        /// font library.
        /// </summary>
        /// <param name="registered">The registered Office font library, or null.</param>
        public static void PublishAndRegister(Tenant tenant, ClientContext adminContext, OrgAssetsLibrary registered, string libraryUrl,
            bool keepRegistration, Action publish, Action<string> warn, Action<string> verbose)
        {
            if (registered != null && keepRegistration)
            {
                publish();
                return;
            }

            if (registered == null)
            {
                // Nothing to restore: register the library only once the fonts and catalog are in place,
                // so Office never reads a library without a complete catalog.
                publish();
                Register(tenant, adminContext, libraryUrl, null, verbose);
                warn(PropagationWarning);
                return;
            }

            // Keep the thumbnail across the remove and add.
            var thumbnailUrl = registered.ThumbnailUrl?.DecodedUrl;
            verbose($"Removing the Office font library registration of {libraryUrl}");
            // The CDN origin stays: the add below reuses it.
            tenant.RemoveFromOrgAssetsAndCdn(false, SPOTenantCdnType.Public, libraryUrl);
            adminContext.ExecuteQueryRetry();
            try
            {
                publish();
            }
            catch (Exception publishError)
            {
                // Put the registration back, so a failed publish never leaves the tenant without its
                // font library, and keep the publish error as the one the user sees.
                try
                {
                    Register(tenant, adminContext, libraryUrl, thumbnailUrl, verbose);
                }
                catch (Exception registerError)
                {
                    throw new PSInvalidOperationException(
                        $"{publishError.Message} Registering {libraryUrl} as the Office font library again also failed: {registerError.Message}", publishError);
                }
                throw;
            }
            Register(tenant, adminContext, libraryUrl, thumbnailUrl, verbose);
            warn(PropagationWarning);
        }

        private static void Register(Tenant tenant, ClientContext adminContext, string libraryUrl, string thumbnailUrl, Action<string> verbose)
        {
            verbose($"Registering {libraryUrl} as the Office font library");
            // Copilot search is only valid for ImageDocumentLibrary: see AddOrgAssetsLibrary.
            var config = new OrgAssetsLibraryConfigParam { IsCopilotSearchable = false, IsCopilotSearchablePresent = false };
            tenant.AddToOrgAssetsLibWithConfig(SPOTenantCdnType.Public, libraryUrl, thumbnailUrl, OrgAssetType.OfficeFontLibrary, false, config);
            adminContext.ExecuteQueryRetry();
        }
    }
}
