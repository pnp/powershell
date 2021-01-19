using System;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class ClassicWebPartPipeBind
    {
        private readonly Guid _id;
        private readonly string _title;

        public ClassicWebPartPipeBind(Guid guid)
        {
            _id = guid;
        }

        public ClassicWebPartPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _title = id;
            }
        }

        public Guid Id => _id;

        public string Title => _title;

        public ClassicWebPartPipeBind()
        {
            _id = Guid.Empty;
            _title = string.Empty;
        }
    }
}
