using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class CompanyPortalSiteDetails
    {
        public string HomeSiteUrl { get; set; }

        public bool DraftMode { get; set; }

        public bool VivaConnectionsDefaultStart { get; set; }
    }
}
