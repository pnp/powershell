using Microsoft.SharePoint.Client;
using System;
using System.Management.Automation;
using PnPCore = PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ListPipeBind
    {
        private readonly List _list;

        private readonly PnPCore.IList _ilist;
        private readonly Guid _id;
        private readonly string _name;

        public ListPipeBind(List list)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public ListPipeBind(Guid guid)
        {
            _id = guid;
        }

        public ListPipeBind(PnPCore.IList list)
        {
            _ilist = list;
        }

        public ListPipeBind(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (!Guid.TryParse(id, out _id))
                _name = id;
        }

        internal PnPCore.IList GetList(PnP.Core.Services.PnPContext context, params System.Linq.Expressions.Expression<Func<PnPCore.IList, object>>[] retrievals)
        {
            PnPCore.IList list = null;
            if (_ilist != null)
            {
                list = _ilist;
                if (retrievals.Length > 0)
                {
                    list.Get(retrievals);
                }
            }
            else if (_id != Guid.Empty)
            {
                list = context.Web.Lists.GetById(_id, retrievals);
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                list = context.Web.Lists.GetByTitle(_name, retrievals);
                if (list == null)
                {
                    list = context.Web.Lists.GetByServerRelativeUrl(_name, retrievals);
                }
            }
            list.EnsurePropertiesAsync(l => l.RootFolder);
            list.RootFolder.EnsurePropertiesAsync(f => f.ServerRelativeUrl);
            return list;
        }

        internal List GetList(Web web, params System.Linq.Expressions.Expression<Func<List, object>>[] retrievals)
        {
            List list = null;
            if (_list != null)
            {
                list = _list;
            }
            else if (_id != Guid.Empty)
            {
                list = web.Lists.GetById(_id);
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                list = web.GetListByTitle(_name);
                if (list == null)
                {
                    list = web.GetListByUrl(_name);
                }
            }
            if (list != null)
            {
                web.Context.Load(list, l => l.Id, l => l.BaseTemplate, l => l.OnQuickLaunch, l => l.DefaultViewUrl, l => l.Title, l => l.Hidden, l => l.ContentTypesEnabled, l => l.RootFolder.ServerRelativeUrl);
                if (retrievals != null)
                {
                    web.Context.Load(list, retrievals);
                }
                web.Context.ExecuteQueryRetry();
            }
            return list;
        }

        internal List GetListOrThrow(string paramName, Web selectedWeb, params System.Linq.Expressions.Expression<Func<List, object>>[] retrievals)
            => GetList(selectedWeb, retrievals)
            ?? throw new PSArgumentException(NoListMessage, paramName);

        internal List GetListOrWarn(Cmdlet cmdlet, Web web, params System.Linq.Expressions.Expression<Func<List, object>>[] retrievals)
        {
            var list = GetList(web, retrievals);
            if (list is null)
                cmdlet.WriteWarning(NoListMessage);

            return list;
        }

        private string NoListMessage
            => $"No list found with id, title or url '{this}' (title is case-sensitive)";

        // public override string ToString()
        //     => _name
        //     ?? (_id != Guid.Empty ? _id.ToString() : null)
        //     ?? (_list.IsPropertyAvailable(l => l.Title) ? _list.Title : null)
        //     ?? (_list.IsPropertyAvailable(l => l.Id) ? _list.Id.ToString() : null)
        //     ?? "[List object with no Title or Id]";
    }
}
