using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TenantSiteScriptPipeBind
    {
        private readonly Guid _id;
        private readonly string _title;
        private readonly TenantSiteScript _siteScript;

        public TenantSiteScriptPipeBind(Guid guid)
        {
            _id = guid;
        }

        public TenantSiteScriptPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public TenantSiteScriptPipeBind(TenantSiteScript siteScript)
        {
            _siteScript = siteScript;
        }

        public Guid Id
        {
            get
            {
                if(_siteScript != null)
                {
                    return _siteScript.Id;
                } else
                {
                    return _id;
                }
            }
        }

        public TenantSiteScriptPipeBind()
        {
            _id = Guid.Empty;
            _siteScript = null;
        }

        public TenantSiteScript[] GetTenantSiteScript(Tenant tenant)
        {
            if (_siteScript != null)
            {
                return new[] { _siteScript };
            }
            if (!string.IsNullOrEmpty(_title))
            {
                var scripts = tenant.GetSiteScripts();
                var result = tenant.Context.LoadQuery(scripts.Where(s => s.Title == _title));
                (tenant.Context as ClientContext).ExecuteQueryRetry();
                return result.ToArray();
            }
            else if (_id != Guid.Empty)
            {
                try
                {
                  var script = Tenant.GetSiteScript(tenant.Context, Id);
                  tenant.Context.Load(script);
                  (tenant.Context as ClientContext).ExecuteQueryRetry();
                  return new[] { script };
                }
                catch(Microsoft.SharePoint.Client.ServerException e) when (e.ServerErrorTypeName == "System.IO.FileNotFoundException")
                {
                    return null;
                }
            }
            return null;
        }
    }
}