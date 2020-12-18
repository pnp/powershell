using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Extensions;
using System.Linq.Expressions;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class WebPipeBind
    {
        private readonly Guid _id;
        private string _url;
        private readonly Web _web;

        public WebPipeBind()
        {
            _id = Guid.Empty;
            _url = string.Empty;
            _web = null;
        }

        public WebPipeBind(Guid guid)
        {
            _id = guid;
        }

        public WebPipeBind(string urlorid)
        {
            if (!Guid.TryParse(urlorid, out _id))
            {
                _url = urlorid;
            }
        }

        public WebPipeBind(Web web)
        {
            _web = web;
        }

        public Web GetWeb(ClientContext clientContext, Expression<Func<Web, object>>[] expressions = null)
        {
            var context = clientContext;

            if (_web != null)
            {
                if (expressions != null)
                {
                    _web.EnsureProperties(expressions);
                }
                return _web;
            }

            if (_id != Guid.Empty)
            {
                return clientContext.Web.GetWebById(_id, expressions);
            }
            if (!string.IsNullOrEmpty(_url))
            {
                if (_url.StartsWith("/"))
                {
                    _url = _url.TrimStart('/');
                }
                return clientContext.Web.GetWebByUrl(_url, expressions);
            }
            return null;
        }
    }
}
