using System;
using Microsoft.SharePoint.Client.Taxonomy;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class GenericObjectNameIdPipeBind<T>
    {
        private readonly Guid _id;

        public GenericObjectNameIdPipeBind(Guid guid)
        {
            _id = guid;
        }

        public GenericObjectNameIdPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                StringValue = id;
            }
        }

        public GenericObjectNameIdPipeBind(T item)
        {
            Item = item;
        }

        public Guid IdValue => _id;

        public string StringValue { get; }

        public T Item { get; }

        public GenericObjectNameIdPipeBind()
        {
            _id = Guid.Empty;
            StringValue = string.Empty;
            Item = default(T);
        }
    }
}
