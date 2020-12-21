using System;
using System.Collections.Generic;
using System.Linq;
using PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class WebPartPipeBind
    {
        private readonly Guid _instanceId;
        private readonly string _title;

        public WebPartPipeBind(Guid guid)
        {
            _instanceId = guid;
        }

        public WebPartPipeBind(string instanceId)
        {
            if (!Guid.TryParse(instanceId, out _instanceId))
            {
                _title = instanceId;
            }
        }

        public Guid InstanceId => _instanceId;

        public string Title => _title;

        public WebPartPipeBind()
        {
            _instanceId = Guid.Empty;
            _title = string.Empty;
        }

        public List<IPageComponent> GetWebPart(IPage page)
        {
            if (page == null)
            {
                throw new ArgumentException(nameof(page));
            }
            if (!string.IsNullOrEmpty(_title))
            {
                return page.Controls.Where(c => c.GetType() == typeof(IPageComponent) && ((IPageComponent)c).Name.Equals(_title, StringComparison.InvariantCultureIgnoreCase)).Cast<IPageComponent>().ToList();
            }
            return page.Controls.Where(c => c.InstanceId == _instanceId).Cast<IPageComponent>().ToList();
        }
    }
}