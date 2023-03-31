using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class VivaACEPipeBind
    {
        private readonly AdaptiveCardExtension _ace;
        private readonly Guid _id;
        private readonly string _title;

        public VivaACEPipeBind()
        {
            _ace = null;
            _id = Guid.Empty;
            _title = string.Empty;
        }

        public VivaACEPipeBind(AdaptiveCardExtension ace)
        {
            _ace = ace;
        }

        public VivaACEPipeBind(Guid guid)
        {
            _id = guid;
        }

        public VivaACEPipeBind(string title)
        {
            if (!Guid.TryParse(title, out _id))
            {
                _title = title;
            }
        }

        public AdaptiveCardExtension GetACE(IVivaDashboard dashboard, PnPWebCmdlet cmdlet)
        {
            AdaptiveCardExtension aceToReturn = null;
            if (!string.IsNullOrEmpty(_title))
            {
                if (dashboard.ACEs.Count(p => p.Title.Equals(_title, StringComparison.Ordinal)) == 1)
                {
                    aceToReturn = dashboard.ACEs.FirstOrDefault(p => p.Title.Equals(_title, StringComparison.Ordinal));
                }
                else
                {
                    cmdlet.WriteWarning("Multiple ACEs found with the same title, please use instance id");
                }
            }
            if (_id != Guid.Empty)
            {
                aceToReturn = dashboard.ACEs.FirstOrDefault(p => p.InstanceId == _id);
                if (aceToReturn == null)
                {
                    // try to find it by the ID
                    // make sure we don't have multiple ACEs of the same type:
                    if (dashboard.ACEs.Count(p => p.Id == _id.ToString()) == 1)
                    {
                        aceToReturn = dashboard.ACEs.FirstOrDefault(p => p.Id == _id.ToString());
                    }
                    else
                    {
                        cmdlet.WriteWarning("Multiple ACEs found of the same type, please use instance id");
                    }
                }
            }
            if (aceToReturn == null)
            {
                aceToReturn = _ace;
            }
            return aceToReturn;
        }
    }
}
