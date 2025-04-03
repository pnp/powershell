using System;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class BrandCenterFontPipeBind
    {
        public readonly Guid? _id;
        public readonly string _title;
        private Font _font;

        public BrandCenterFontPipeBind()
        {
        }

        public BrandCenterFontPipeBind(Guid id)
        {
            _id = id;
        }

        public BrandCenterFontPipeBind(string idOrTitle)
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

        public BrandCenterFontPipeBind(Font font)
        {
            _font = font;
        }

        /// <summary>
        /// Gets the Font represented in this pipebind
        /// </summary>
        /// <param name="cmdlet">The cmdlet instance to use to retrieve the Font in this pipe bind</param>
        /// <param name="clientContext">ClientContext to use to communicate with SharePoint Online</param>
        /// <param name="connection">Connection to use to communicate with SharePoint Online</param>
        /// <param name="webUrl">Url to use to check the site collection Brand Center</param>
        /// <param name="store">The store to check for the font. When NULL, it will check all stores.</param>
        /// <exception cref="Exception">Thrown when the ContainerProperties cannot be retrieved</exception>
        /// <returns>Font</returns>
        public Font GetFont(BasePSCmdlet cmdlet, ClientContext clientContext, PnPConnection connection, string webUrl, Store store = Store.All)
        {
            if (_font != null)
            {
                return _font;
            }
            else if (_id.HasValue)
            {
                _font = BrandCenterUtility.GetFontById(cmdlet, clientContext, connection, _id.Value, webUrl, store);
                return _font;
            }
            else if (!string.IsNullOrEmpty(_title))
            {
                _font = BrandCenterUtility.GetFontByTitle(cmdlet, clientContext, connection, _title, webUrl, store);
                return _font;
            }            
            return null;
        }
    }
}