using System;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class TaxonomyTermStorePipeBind
    {
        private readonly Guid _id = Guid.Empty;
        private readonly string _title = string.Empty;
        private readonly TermStore _item = null;

        public TaxonomyTermStorePipeBind(Guid guid)
        {
            _id = guid;
        }

        public TaxonomyTermStorePipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public TaxonomyTermStorePipeBind(TermStore item)
        {
            _item = item;
        }

        public Guid Id => _id;

        public string Title => _title;

        public TermStore Item => _item;

        internal TermStore GetTermStore(TaxonomySession taxonomySession)
        {
            if (_title != null)
            {
                return taxonomySession.TermStores.GetByName(_title);
            }
            else if (_id != Guid.Empty)
            {
                return taxonomySession.TermStores.GetById(_id);
            }
            else if (_item != null)
            {
                return _item;
            }
            return null;
        }
    }
}