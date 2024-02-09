using PnP.Core.Model;
using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PagesUtility
    {
        private static readonly Expression<Func<IList, object>>[] getPagesLibraryExpression = new Expression<Func<IList, object>>[] {p => p.Title, p => p.TemplateType, p => p.Id,
            p => p.RootFolder.QueryProperties(p => p.Properties, p => p.ServerRelativeUrl), p => p.Fields, p => p.ListItemEntityTypeFullName };
        internal static IList GetModernPagesLibrary(IWeb web)
        {
            IList pagesLibrary = null;
            var libraries = web.Lists.QueryProperties(getPagesLibraryExpression)
                                                       .Where(p => p.TemplateType == ListTemplateType.WebPageLibrary)
                                                       .Where(p => p.ListItemEntityTypeFullName == "SP.Data.SitePagesItem")
                                                       .ToListAsync()
                                                       .GetAwaiter().GetResult();

            if (libraries.Count == 1)
            {
                pagesLibrary = libraries[0];
            }
            else
            {
                foreach (var list in libraries)
                {
                    if (list.ListItemEntityTypeFullName == "SP.Data.SitePagesItem" && list.IsPropertyAvailable(p => p.Fields) && list.Fields.AsRequested().FirstOrDefault(p => p.InternalName == "CanvasContent1") != null)
                    {
                        pagesLibrary = list;
                        break;
                    }
                }
            }

            return pagesLibrary;
        }
    }
}
