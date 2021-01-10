using System;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class TaxonomyTermGroupPipeBind
    {
        private readonly Guid _id = Guid.Empty;
        private readonly string _title = string.Empty;
        private readonly TermGroup _item = null;

        public TaxonomyTermGroupPipeBind(Guid guid)
        {
            _id = guid;
        }

        public TaxonomyTermGroupPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public TaxonomyTermGroupPipeBind(TermGroup item)
        {
            _item = item;
            _id = item.Id;
        }

        public Guid Id => _id;

        public string Title => _title;

        public TermGroup Item => _item as TermGroup;

        internal TermGroup GetGroup(TermStore termStore)
        {
            if (_item != null)
            {
                return Item;
            }
            if (_id != Guid.Empty)
            {
                return termStore.Groups.GetById(Id);
            }
            else if (!string.IsNullOrEmpty(_title))
            {
                return termStore.Groups.GetByName(_title);
            }
            return null;
        }
    }
}
