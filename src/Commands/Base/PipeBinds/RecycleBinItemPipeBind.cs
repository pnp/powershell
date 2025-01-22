using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class RecycleBinItemPipeBind
    {
        private RecycleBinItem _item;
        private IRecycleBinItem _recycleBinItem;
        private readonly Guid? _id;

        public RecycleBinItemPipeBind()
        {
            _item = null;
            _id = null;
            _recycleBinItem = null;
        }

        public RecycleBinItemPipeBind(RecycleBinItem item)
        {
            _item = item;
            _id = item?.Id;
        }

        public RecycleBinItemPipeBind(RecycleResult result)
        {
            _id = result.RecycleBinItemId;
        }

        public RecycleBinItemPipeBind(IRecycleBinItem result)
        {
            _recycleBinItem = result;
            _id = result?.Id;
        }

        public RecycleBinItemPipeBind(Guid guid)
        {
            _id = guid;
        }

        public RecycleBinItemPipeBind(string id)
        {
            Guid guid;

            if (Guid.TryParse(id, out guid))
            {
                _id = guid;
            }
            else
            {
                _id = null;
            }
        }

        public RecycleBinItem Item => _item;

        public IRecycleBinItem RecycleBinItem => _recycleBinItem;

        public Guid? Id => _id;

        internal RecycleBinItem GetRecycleBinItem(Microsoft.SharePoint.Client.Site site)
        {
            if (Item != null) return Item;
            if (!_id.HasValue) return null;

            _item = site.RecycleBin.GetById(_id.Value);
            site.Context.Load(_item);
            site.Context.ExecuteQueryRetry();
            return Item;
        }

        internal IRecycleBinItem GetRecycleBinItem(Core.Services.PnPContext context)
        {
            if (RecycleBinItem != null) return RecycleBinItem;
            if (!_id.HasValue) return null;

            _recycleBinItem = context.Site.RecycleBin.GetById(_id.Value, r => r.LeafName);

            return RecycleBinItem;
        }
    }
}
