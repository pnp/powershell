using Microsoft.SharePoint.Client;
using PnPCore = PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.ClientSidePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PageComponentPipeBind
    {
        private readonly PnPCore.IPageComponent _component;
        private string _name;
        private Guid _id;

        public PageComponentPipeBind(PnPCore.IPageComponent component)
        {
            _component = component;
            _id = Guid.Parse(_component.Id);
            _name = _component.Name;
        }

        public PageComponentPipeBind(string nameOrId)
        {
            _component = null;
            if (!Guid.TryParse(nameOrId, out _id))
            {
                _name = nameOrId;
            }
        }

        public PageComponentPipeBind(Guid id)
        {
            _id = id;
            _name = null;
            _component = null;
        }

        public PnPCore.IPageComponent Component => _component;

        public string Name => _component?.Name;

        public string Id => _component == null ? Guid.Empty.ToString() : _component.Id;

        public override string ToString() => Name;

        internal PnPCore.IPageComponent GetComponent(PnPCore.IPage page)
        {
            if (_component != null)
            {
                return _component;
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                PnPCore.IPageComponent com = page.AvailablePageComponents(_name).FirstOrDefault();
                return com;
            }
            else if (_id != Guid.Empty)
            {
                string idAsString = _id.ToString();
                var comQuery = from c in page.AvailablePageComponents(_name)
                               where c.Id == idAsString
                               select c;
                return comQuery.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}