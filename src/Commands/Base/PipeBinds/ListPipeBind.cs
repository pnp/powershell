using Microsoft.SharePoint.Client;
using PnPCore = PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ListPipeBind
    {
        public List ListInstance { get; private set; }
        private readonly PnPCore.IList _corelist;
        private readonly Guid _id;
        private readonly string _name;

        public ListPipeBind(List list)
        {
            ListInstance = list ?? throw new ArgumentNullException(nameof(list));
        }

        public ListPipeBind(Guid guid)
        {
            _id = guid;
        }

        public ListPipeBind(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (!Guid.TryParse(id, out _id))
            {
                _name = id;
            }
        }

        public ListPipeBind(PnPCore.IList list)
        {
            _corelist = list ?? throw new ArgumentNullException(nameof(list));
        }

        internal List GetList(Web web, params System.Linq.Expressions.Expression<Func<List, object>>[] retrievals)
        {
            List list = null;
            if (ListInstance != null)
            {
                list = ListInstance;
            }
            else if (_id != Guid.Empty)
            {
                list = web.Lists.GetById(_id);
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                list = web.GetListByUrl(_name);
                if(list.ServerObjectIsNull())
                {
                    list = web.GetListByTitle(_name);
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

        internal PnPCore.IList GetList(PnPBatch batch, params System.Linq.Expressions.Expression<Func<PnPCore.IList, object>>[] selectors)
        {
            return GetList(batch, true, selectors);
        }

        internal PnPCore.IList GetList(PnPBatch batch, bool throwError = true, params System.Linq.Expressions.Expression<Func<PnPCore.IList, object>>[] selectors)
        {
            PnPCore.IList returnList = null;
            if (_corelist != null)
            {
                returnList = _corelist;
            }
            if (ListInstance != null)
            {
                var batchedList = batch.GetCachedList(ListInstance.Id);
                if (batchedList != null)
                {
                    return batchedList;
                }
                returnList = batch.Context.Web.Lists.GetById(ListInstance.Id, selectors);
            }
            else if (_id != Guid.Empty)
            {
                var batchedList = batch.GetCachedList(_id);
                if (batchedList != null)
                {
                    return batchedList;
                }
                returnList = batch.Context.Web.Lists.GetById(_id, selectors);
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                var batchedList = batch.GetCachedList(_name);
                if (batchedList != null)
                {
                    return batchedList;
                }
                returnList = batch.Context.Web.Lists.GetByTitle(_name, selectors);
                if (returnList == null)
                {
                    var url = _name;
                    batch.Context.Web.EnsureProperties(w => w.ServerRelativeUrl);
                    if (!_name.ToLower().StartsWith(batch.Context.Web.ServerRelativeUrl.ToLower()))
                    {
                        url = $"{batch.Context.Web.ServerRelativeUrl}/{url.TrimStart('/')}";
                    }
                    try
                    {
                        batchedList = batch.GetCachedList(url);
                        if (batchedList != null)
                        {
                            return batchedList;
                        }
                        returnList = batch.Context.Web.Lists.GetByServerRelativeUrl(url, selectors);
                    }
                    catch (PnP.Core.SharePointRestServiceException e) when ((e.Error as PnP.Core.SharePointRestError)?.Code == "System.IO.FileNotFoundException" && !throwError)
                    {
                        return null;
                    }
                    catch (PnP.Core.SharePointRestServiceException ex)
                    {
                        throw new PSInvalidOperationException((ex.Error as PnP.Core.SharePointRestError).Message);
                    }
                }
            }
            if (returnList != null)
            {
                returnList.EnsureProperties(l => l.Id, l => l.OnQuickLaunch, l => l.Title, l => l.Hidden, l => l.ContentTypesEnabled, l => l.RootFolder, l => l.Fields.QueryProperties(f => f.Id, f => f.Title, f => f.InternalName, f => f.TypeAsString));
                batch.CacheList(returnList);
            }
            return returnList;
        }

        internal PnPCore.IList GetList(PnP.Core.Services.PnPContext context, params System.Linq.Expressions.Expression<Func<PnPCore.IList, object>>[] selectors)
        {
            PnPCore.IList returnList = null;
            if (_corelist != null)
            {
                returnList = _corelist;
            }
            if (ListInstance != null)
            {
                returnList = context.Web.Lists.GetById(ListInstance.Id, selectors);
            }
            else if (_id != Guid.Empty)
            {
                returnList = context.Web.Lists.GetById(_id, selectors);
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                returnList = context.Web.Lists.GetByTitle(_name, selectors);
                if (returnList == null)
                {
                    var url = _name;
                    context.Web.EnsureProperties(w => w.ServerRelativeUrl);
                    if (!_name.ToLower().StartsWith(context.Web.ServerRelativeUrl.ToLower()))
                    {
                        url = $"{context.Web.ServerRelativeUrl}/{url.TrimStart('/')}";
                    }
                    try
                    {
                        returnList = context.Web.Lists.GetByServerRelativeUrl(url, selectors);
                    }
                    catch (PnP.Core.SharePointRestServiceException)
                    {
                        throw new PSInvalidOperationException("List not found");
                    }
                }
            }
            if (returnList != null)
            {
                returnList.EnsureProperties(l => l.Id, l => l.OnQuickLaunch, l => l.Title, l => l.Hidden, l => l.ContentTypesEnabled, l => l.RootFolder);
            }
            return returnList;
        }

        internal List GetListOrThrow(string paramName, Web selectedWeb, params System.Linq.Expressions.Expression<Func<List, object>>[] retrievals)
        {
            return GetList(selectedWeb, retrievals) ?? throw new PSArgumentException(NoListMessage, paramName);
        }

        internal PnPCore.IList GetListOrThrow(string paramName, PnP.Core.Services.PnPContext context, params System.Linq.Expressions.Expression<Func<PnPCore.IList, object>>[] retrievals)
        {
            return GetList(context, retrievals) ?? throw new PSArgumentException(NoListMessage, paramName);
        }

        internal List GetListOrWarn(Cmdlet cmdlet, Web web, params System.Linq.Expressions.Expression<Func<List, object>>[] retrievals)
        {
            var list = GetList(web, retrievals);
            if (list is null)
            {
                cmdlet.WriteWarning(NoListMessage);
            }

            return list;
        }

        private string NoListMessage => $"No list found with id, title or url '{this}' (title is case-sensitive)";

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_name))
            {
                return _name;
            }
            if (_corelist != null)
            {
                return _corelist.Title;
            }
            if (ListInstance != null)
            {
                return ListInstance.Title;
            }
            return "Unknown list";
        }
    }
}