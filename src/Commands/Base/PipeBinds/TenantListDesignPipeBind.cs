using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class TenantListDesignPipeBind
    {
        private readonly Guid _id;
        private readonly string _title;
        private readonly TenantListDesign _listDesign;

        public TenantListDesignPipeBind(Guid guid)
        {
            _id = guid;
        }

        public TenantListDesignPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public TenantListDesignPipeBind(TenantListDesign listDesign)
        {
            _listDesign = listDesign;
        }

        public Guid Id
        {
            get
            {
                if (_listDesign != null)
                {
                    return _listDesign.Id;
                }
                else
                {
                    return _id;
                }
            }
        }

        public TenantListDesign[] GetTenantListDesign(Tenant tenant)
        {
            if (_listDesign != null)
            {
                return new[] { _listDesign };
            }
            if (!string.IsNullOrEmpty(_title))
            {
                var designs = tenant.GetListDesigns();
                var result = tenant.Context.LoadQuery(designs.Where(d => d.Title == _title));
                (tenant.Context as ClientContext).ExecuteQueryRetry();
                return result.ToArray();
            }
            else if (_id != Guid.Empty)
            {
                try
                {
                  var design = Tenant.GetListDesign(tenant.Context, Id);
                  tenant.Context.Load(design);
                  (tenant.Context as ClientContext).ExecuteQueryRetry();
                  return new[] { design };
                }
                catch(Microsoft.SharePoint.Client.ServerException e) when (e.ServerErrorTypeName == "System.IO.FileNotFoundException")
                {
                    return null;
                }
            }
            return null;
        }

        public TenantListDesignPipeBind()
        {
            _id = Guid.Empty;
            _listDesign = null;
        }

    }
}