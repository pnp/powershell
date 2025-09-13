using Microsoft.Office.SharePoint.Tools;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utilities for working with the Brand Center
    /// </summary>
    internal static class BrandCenterUtility
    {
        /// <summary>
        /// Retrieves a font package from the Brand Center based on its name and optionally store type.
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="title">Title of the font to retrieve</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <returns>The font with the provided identity or NULL if no font found with the provided identity</returns>
        public static FontPackage GetFontPackageByTitle(BasePSCmdlet cmdlet, ClientContext clientContext, string title, string webUrl, Store store = Store.All)
        {
            if (store == Store.All)
            {
                return GetFontPackageByTitle(cmdlet, clientContext, title, webUrl, Store.Site) ??
                       GetFontPackageByTitle(cmdlet, clientContext, title, webUrl, Store.Tenant) ??
                       GetFontPackageByTitle(cmdlet, clientContext, title, webUrl, Store.OutOfBox);
            }

            var url = $"{GetStoreFontPackageUrlByStoreType(store, webUrl)}/GetByTitle('{title}')";
            cmdlet?.LogDebug($"Making a GET request to {url} to retrieve {store} font with title {title}.");
            try
            {
                var font = RestHelper.Get<FontPackage>(Framework.Http.PnPHttpClient.Instance.GetHttpClient(), url, clientContext);

                if (font != null)
                {
                    return font;
                }
            }
            catch (HttpRequestException ex)
            {
                cmdlet?.LogDebug($"Font with title {title} not found in the {store} Brand Center: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Retrieves a font package from the Brand Center based on its identity and optionally store type.
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="identity">Id of the font to retrieve</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <returns>The font with the provided identity or NULL if no font found with the provided identity</returns>
        public static FontPackage GetFontPackageById(BasePSCmdlet cmdlet, ClientContext clientContext, Guid identity, string webUrl, Store store = Store.All)
        {
            if (store == Store.All)
            {
                return GetFontPackageById(cmdlet, clientContext, identity, webUrl, Store.Site) ??
                       GetFontPackageById(cmdlet, clientContext, identity, webUrl, Store.Tenant) ??
                       GetFontPackageById(cmdlet, clientContext, identity, webUrl, Store.OutOfBox);
            }

            var url = $"{GetStoreFontPackageUrlByStoreType(store, webUrl)}/GetById('{identity}')";
            cmdlet?.LogDebug($"Making a GET request to {url} to retrieve {store} font with identity {identity}.");
            try
            {
                var font = RestHelper.Get<FontPackage>(Framework.Http.PnPHttpClient.Instance.GetHttpClient(), url, clientContext);

                if (font != null)
                {
                    return font;
                }
            }
            catch (HttpRequestException ex)
            {
                cmdlet?.LogDebug($"Font with {identity} not found in the {store} Brand Center: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Retrieves font packages from the Brand Center optionally based on the store type.
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <returns>The available fonts</returns>
        public static List<FontPackage> GetFontPackages(BasePSCmdlet cmdlet, ClientContext clientContext, string webUrl, Store store = Store.All)
        {
            if (store == Store.All)
            {
                var allStoresFonts = new List<FontPackage>();
                allStoresFonts.AddRange(GetFontPackages(cmdlet, clientContext, webUrl, Store.Site));
                allStoresFonts.AddRange(GetFontPackages(cmdlet, clientContext, webUrl, Store.Tenant));
                allStoresFonts.AddRange(GetFontPackages(cmdlet, clientContext, webUrl, Store.OutOfBox));
                return allStoresFonts;
            }

            var url = GetStoreFontPackageUrlByStoreType(store, webUrl);
            cmdlet?.LogDebug($"Making a GET request to {url} to retrieve {store} fonts.");
            var fonts = RestHelper.Get<RestResultCollection<FontPackage>>(Framework.Http.PnPHttpClient.Instance.GetHttpClient(), url, clientContext);
            return fonts.Items.ToList();
        }

        /// <summary>
        /// Adds a font package to the Brand Center
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="webUrl">Url to use to add the font package to the Brand Center</param>
        /// <param name="store">The store add the font package to. Only Tenant and Site are allowed.</param>
        /// <param name="title">Title of the font package to add</param>
        /// <param name="visble">Indicates if the font package should be visible in the Brand Center. Defaults to true.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <returns>The created FontPackage instance</returns>
        public static FontPackage AddFontPackage(BasePSCmdlet cmdlet, ClientContext clientContext, Store store, string webUrl, string title, Font displayFont, Font contentFont, Font titleFont, string titleFontStyle, Font headlineFont, string headlineFontStyle, Font bodyFont, string bodyFontStyle, Font interactiveFont, string interactiveFontStyle, bool visble = true)
        {
            if (store != Store.Tenant && store != Store.Site)
            {
                throw new ArgumentOutOfRangeException(nameof(store), store, "Only Tenant and Site stores are supported for adding font packages.");
            }
            if (string.IsNullOrEmpty(webUrl))
            {
                throw new ArgumentNullException(nameof(webUrl), "Web URL cannot be null or empty.");
            }

            // Validate that the provided fonts are all either the displayFont or contentFont
            if (bodyFont.Id != displayFont.Id && bodyFont.Id != contentFont.Id)
            {
                throw new ArgumentException($"{nameof(bodyFont)} must be either the {nameof(bodyFont)} or {nameof(contentFont)} font.");
            }
            if (headlineFont.Id != displayFont.Id && headlineFont.Id != contentFont.Id)
            {
                throw new ArgumentException($"{nameof(headlineFont)} must be either the {nameof(displayFont)} or {nameof(contentFont)} font.");
            }
            if (interactiveFont.Id != displayFont.Id && interactiveFont.Id != contentFont.Id)
            {
                throw new ArgumentException($"{nameof(interactiveFont)} must be either the {nameof(displayFont)} or {nameof(contentFont)} font.");
            }
            if (titleFont.Id != displayFont.Id && titleFont.Id != contentFont.Id)
            {
                throw new ArgumentException($"{nameof(titleFont)} must be either the {nameof(displayFont)} or {nameof(contentFont)} font.");
            }

            // Generate unique IDs for the fonts to avoid conflicts
            var displayFontUniqueId = $"{displayFont.Name}-{new Random().NextInt64(1000000000L, 9999999999L)}";
            var contentFontUniqueId = $"{contentFont.Name}-{new Random().NextInt64(1000000000L, 9999999999L)}";

            // Create the font package
            var url = GetStoreFontPackageUrlByStoreType(store, webUrl);
            cmdlet?.LogDebug($"Making a POST request to {url} to create a {store} Brand Center font package.");
            var fontpackage = RestHelper.Post<FontPackage>(Framework.Http.PnPHttpClient.Instance.GetHttpClient(), url, clientContext, new FontPackage
            {
                Title = title,
                Store = store,
                PackageJson = System.Text.Json.JsonSerializer.Serialize(new
                {
                    fontFaces = new[]
                    {
                        new { fontFamily = displayFontUniqueId, fontType = "displayFont", path = displayFont.FileName },
                        new { fontFamily = contentFontUniqueId, fontType = "contentFont", path = contentFont.FileName }
                    },
                    fontSlots = new
                    {
                        body = new { fontFace = bodyFontStyle, fontFamily = bodyFont.Id == displayFont.Id ? displayFontUniqueId : contentFontUniqueId },
                        heading = new { fontFace = headlineFontStyle, fontFamily = headlineFont.Id == displayFont.Id ? displayFontUniqueId : contentFontUniqueId },
                        label = new { fontFace = interactiveFontStyle, fontFamily = interactiveFont.Id == displayFont.Id ? displayFontUniqueId : contentFontUniqueId },
                        title = new { fontFace = titleFontStyle, fontFamily = titleFont.Id == displayFont.Id ? displayFontUniqueId : contentFontUniqueId }
                    }
                }),
                IsValid = true,
                IsHidden = !visble
            });

            return fontpackage;
        }

        /// <summary>
        /// Returns the font package URL to the Brand Center based on the store type
        /// </summary>
        /// <param name="store">Brand Center store to connect to</param>
        /// <param name="webUrl">Base URL to connect to</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if an invalid store type has been provided</exception>
        public static string GetStoreFontPackageUrlByStoreType(Store store, string webUrl)
        {
            return store switch
            {
                Store.Tenant => $"{webUrl}/_api/FontPackages",
                Store.Site => $"{webUrl}/_api/SiteFontPackages",
                Store.OutOfBox => $"{webUrl}/_api/outofboxfontpackages",
                _ => throw new ArgumentOutOfRangeException(nameof(store), store, null)
            };
        }

        /// <summary>
        /// Retrieves all fonts from the Brand Center fonts library
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <returns>List with Font instances</returns>
        public static List<Font> GetFonts(BasePSCmdlet cmdlet, ClientContext clientContext)
        {
            var brandCenterConfig = GetBrandCenterConfiguration(cmdlet, clientContext);

            if (brandCenterConfig == null || string.IsNullOrEmpty(brandCenterConfig.SiteUrl) || string.IsNullOrEmpty(brandCenterConfig.BrandFontLibraryUrl.DecodedUrl))
            {
                cmdlet?.LogError("Brand Center configuration is not available or incomplete.");
                return null;
            }

            cmdlet?.LogDebug("Retrieving all fonts from the Brand Center fonts library at {brandCenterConfig.BrandFontLibraryUrl.DecodedUrl}");
            var url = $"{brandCenterConfig.SiteUrl}/_api/SP.List.GetListDataAsStream?listFullUrl='{brandCenterConfig.BrandFontLibraryUrl.DecodedUrl}'";
            // Data is in a Row property
            var fonts = RestHelper.Post<RestRowCollection<Font>>(Framework.Http.PnPHttpClient.Instance.GetHttpClient(), url, clientContext, new
            {
                parameters = new
                {
                    ViewXml = "<View><ViewFields><FieldRef Name=\"FileLeafRef\"/><FieldRef Name=\"_SPFontVisible\"/><FieldRef Name=\"_SPFontFamilyName\"/><FieldRef Name=\"_SPFontFaces\"/></ViewFields><Query><Where><And><IsNotNull><FieldRef Name=\"_SPFontFamilyName\"/></IsNotNull><IsNotNull><FieldRef Name=\"_SPFontFaces\"/></IsNotNull></And></Where></Query><RowLimit Paged=\"TRUE\">5000</RowLimit></View>"
                }
            });
            return fonts.Items.ToList();
        }

        /// <summary>
        /// Retrieves the Brand Center configuration for the tenant
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <returns>BrandCenterConfiguration instance</returns>
        public static BrandCenterConfiguration GetBrandCenterConfiguration(BasePSCmdlet cmdlet, ClientContext clientContext)
        {
            cmdlet?.LogDebug("Retrieving the Brand Center configuration");
            var brandCenter = new BrandCenter(clientContext);
            var config = brandCenter.CurrentBrandingConfiguration();
            clientContext.ExecuteQueryRetry();

            return config.Value;
        }
    }
}
