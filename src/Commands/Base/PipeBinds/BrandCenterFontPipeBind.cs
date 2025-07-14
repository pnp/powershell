using System;
using System.Linq;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class BrandCenterFontPipeBind
    {
        public readonly int? _id;
        public readonly string _name;
        private Font _font;

        public BrandCenterFontPipeBind()
        {
        }

        public BrandCenterFontPipeBind(int id)
        {
            _id = id;
        }

        public BrandCenterFontPipeBind(string idOrName)
        {
            if(int.TryParse(idOrName, out int intId))
            {
                _id = intId;
            }
            else
            {
                _name = idOrName;
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
        /// <returns>Font</returns>
        public Font GetFont(BasePSCmdlet cmdlet, ClientContext clientContext)
        {
            if (_font != null)
            {
                return _font;
            }
            else if (_id.HasValue)
            {
                _font = BrandCenterUtility.GetFonts(cmdlet, clientContext).FirstOrDefault(f => f.Id == _id.Value.ToString());
                return _font;
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                _font = BrandCenterUtility.GetFonts(cmdlet, clientContext).FirstOrDefault(f => f.Name == _name);
                return _font;
            }            
            return null;
        }
    }
}