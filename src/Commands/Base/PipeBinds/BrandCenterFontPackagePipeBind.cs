using System;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class BrandCenterFontPackagePipeBind
    {
        public readonly Guid? _id;
        public readonly string _title;
        private FontPackage _fontPackage;

        public BrandCenterFontPackagePipeBind()
        {
        }

        public BrandCenterFontPackagePipeBind(Guid id)
        {
            _id = id;
        }

        public BrandCenterFontPackagePipeBind(string idOrTitle)
        {
            if(Guid.TryParse(idOrTitle, out Guid guidId))
            {
                _id = guidId;
            }
            else
            {
                _title = idOrTitle;
            }
        }

        public BrandCenterFontPackagePipeBind(FontPackage fontPackage)
        {
            _fontPackage = fontPackage;
        }

        /// <summary>
        /// Gets the Font Package represented in this pipebind
        /// </summary>
        /// <param name="cmdlet">The cmdlet instance to use to retrieve the Font Package in this pipe bind</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <exception cref="Exception">Thrown when the ContainerProperties cannot be retrieved</exception>
        /// <returns>Font Package</returns>
        public FontPackage GetFontPackage(BasePSCmdlet cmdlet, ClientContext clientContext, string webUrl, Store store = Store.All)
        {
            if (_fontPackage != null)
            {
                return _fontPackage;
            }
            else if (_id.HasValue)
            {
                _fontPackage = BrandCenterUtility.GetFontPackageById(cmdlet, clientContext, _id.Value, webUrl, store);
                return _fontPackage;
            }
            else if (!string.IsNullOrEmpty(_title))
            {
                _fontPackage = BrandCenterUtility.GetFontPackageByTitle(cmdlet, clientContext, _title, webUrl, store);
                return _fontPackage;
            }            
            return null;
        }
    }
}