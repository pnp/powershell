using PnP.Core.Model.Security;
using PnP.Core.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class ListItemVersion
    {
        public int Id { get; set; }

        public bool IsCurrentVersion { get; set; }

        public DateTime Created { get; set; }

        public ISharePointUser CreatedBy { get; set; }

        public IEnumerable<IField> Fields { get; set; }

        public object Values { get; set; }
        public string VersionLabel { get; set; }
    }
}
