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
        /// Retrieves a font from the Brand Center based on its name and optionally store type.
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="title">Title of the font to retrieve</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <param name="connection">Connection to use to communicate with SharePoint Online</param>
        /// <returns>The font with the provided identity or NULL if no font found with the provided identity</returns>
        public static Font GetFontByTitle(BasePSCmdlet cmdlet, ClientContext clientContext, PnPConnection connection, string title, string webUrl, Store store = Store.All)
        {
            if (store == Store.All)
            {
                return GetFontByTitle(cmdlet, clientContext, connection, title, webUrl, Store.Site) ??
                       GetFontByTitle(cmdlet, clientContext, connection, title, webUrl, Store.Tenant) ??
                       GetFontByTitle(cmdlet, clientContext, connection, title, webUrl, Store.OutOfBox);
            }

            var url = $"{GetStoreUrlByStoreType(store, webUrl)}/GetByTitle('{title}')";
            cmdlet?.LogDebug($"Making a GET request to {url} to retrieve {store} font with title {title}.");
            try
            {
                var font = RestHelper.Get<Font>(connection.HttpClient, url, clientContext);

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
        /// Retrieves a font from the Brand Center based on its identity and optionally store type.
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="identity">Id of the font to retrieve</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <param name="connection">Connection to use to communicate with SharePoint Online</param>
        /// <returns>The font with the provided identity or NULL if no font found with the provided identity</returns>
        public static Font GetFontById(BasePSCmdlet cmdlet, ClientContext clientContext, PnPConnection connection, Guid identity, string webUrl, Store store = Store.All)
        {
            if (store == Store.All)
            {
                return GetFontById(cmdlet, clientContext, connection, identity, webUrl, Store.Site) ??
                       GetFontById(cmdlet, clientContext, connection, identity, webUrl, Store.Tenant) ??
                       GetFontById(cmdlet, clientContext, connection, identity, webUrl, Store.OutOfBox);
            }

            var url = $"{GetStoreUrlByStoreType(store, webUrl)}/GetById('{identity}')";
            cmdlet?.LogDebug($"Making a GET request to {url} to retrieve {store} font with identity {identity}.");
            try
            {
                var font = RestHelper.Get<Font>(connection.HttpClient, url, clientContext);

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
        /// Retrieves fonts from the Brand Center optionally based on the store type.
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which this runs, used for logging</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <param name="connection">Connection to use to communicate with SharePoint Online</param>
        /// <returns>The available fonts</returns>
        public static List<Font> GetFonts(BasePSCmdlet cmdlet, ClientContext clientContext, PnPConnection connection, string webUrl, Store store = Store.All)
        {

            if (store == Store.All)
            {
                var allStoresFonts = new List<Font>();
                allStoresFonts.AddRange(GetFonts(cmdlet, clientContext, connection, webUrl, Store.Site));
                allStoresFonts.AddRange(GetFonts(cmdlet, clientContext, connection, webUrl, Store.Tenant));
                allStoresFonts.AddRange(GetFonts(cmdlet, clientContext, connection, webUrl, Store.OutOfBox));
                return allStoresFonts;
            }

            var url = GetStoreUrlByStoreType(store, webUrl);
            cmdlet?.LogDebug($"Making a GET request to {url} to retrieve {store} fonts.");
            var fonts = RestHelper.Get<RestResultCollection<Font>>(connection.HttpClient, url, clientContext);
            return fonts.Items.ToList();
        }

        /// <summary>
        /// Returns the URL to the Brand Center based on the store type
        /// </summary>
        /// <param name="store">Brand Center store to connect to</param>
        /// <param name="webUrl">Base URL to connect to</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if an invalid store type has been provided</exception>
        public static string GetStoreUrlByStoreType(Store store, string webUrl)
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
