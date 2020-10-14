using System;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class TaxonomyTermSetPipeBind
    {
        private readonly Guid _id = Guid.Empty;
        private readonly string _title = string.Empty;
        private readonly TermSet _item = null;

        public TaxonomyTermSetPipeBind(Guid guid)
        {
            _id = guid;
        }

        public TaxonomyTermSetPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public TaxonomyTermSetPipeBind(TermSet item)
        {
            _item = item;
            _id = item.Id;
        }

        public Guid Id => _id;

        public string Title => _title;

        public TermSet Item => _item as TermSet;

        internal TermSet GetTermSet(TermGroup termGroup)
        {
            if (_id != Guid.Empty)
            {
                return termGroup.TermSets.GetById(_id);
            }
            else if (!string.IsNullOrEmpty(_title))
            {
                return termGroup.TermSets.GetByName(_title);
            }
            else if (_item != null)
            {
                return _item;
            }
            return null;
        }
    }
}
