using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PagePipeBind
    {
        private readonly IPage _page;
        private string _name;

        public PagePipeBind(IPage page)
        {
            _page = page;
            _name = string.IsNullOrEmpty(page.Folder) ? page.Name : $"{page.Folder}/{page.Name}";
        }

        public PagePipeBind(string name)
        {
            _page = null;
            _name = name;
        }

        public IPage Page => _page;

        public string Name => PageUtilities.EnsureCorrectPageName(_name);

        public override string ToString() => Name;

        internal IPage GetPage()
        {
            var ctx = PnPConnection.Current.PnPContext;
            if (_page != null)
            {
                return _page;
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                try
                {
                    var pages = ctx.Web.GetPages(Name.TrimStart('/'));
                    if (pages != null)
                    {
                        // Just grab the first returned page, the filtering was already done in the GetPages() call
                        var page = pages.FirstOrDefault();

                        if (page != null)
                        {
                            return page;
                        }
                    }
                    return null;
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}