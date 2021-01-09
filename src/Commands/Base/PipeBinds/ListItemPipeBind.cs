using Microsoft.SharePoint.Client;
using PnPCore = PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{

    public sealed class ListItemPipeBind
    {
        private readonly ListItem _item;
        private readonly PnPCore.IListItem _coreItem;

        private readonly uint _id;

        public ListItemPipeBind()
        {
            _item = null;
            _coreItem = null;
            _id = uint.MinValue;
        }

        public ListItemPipeBind(ListItem item)
        {
            _item = item;
        }

        public ListItemPipeBind(PnPCore.IListItem item)
        {
            _coreItem = item;
        }

        public ListItemPipeBind(string id)
        {
            uint uintId;

            if (uint.TryParse(id, out uintId))
            {
                _id = uintId;
            }
            else
            {
                _id = uint.MinValue;
            }
        }

        public ListItem Item => _item;
        public PnPCore.IListItem CoreItem => _coreItem;

        public uint Id => _id;

        internal ListItem GetListItem(List list)
        {
            ListItem item = null;
            if (_item != null)
            {
                item = _item;
            }
            else if (_id != uint.MinValue)
            {
                item = list.GetItemById((int)_id);
            }

            if (item != null)
            {
                list.Context.Load(item);
                list.Context.ExecuteQueryRetry();
            }
            return item;
        }

        internal PnPCore.IListItem GetListItem(PnPCore.IList list)
        {
            PnPCore.IListItem item = null;
            if (_coreItem != null)
            {
                item = _coreItem;
            }
            if (_id != uint.MinValue)
            {
                item = list.Items.GetById((int)_id);
            }
            return item;
        }

    }
}
