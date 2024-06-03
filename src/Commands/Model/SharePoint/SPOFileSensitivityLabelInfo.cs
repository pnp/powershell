using Microsoft.Online.SharePoint.TenantAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class SPOFileSensitivityLabelInfo
    {
        public string LabelId { get; set; }

        public string DisplayName { get; set; }

        public bool ProtectionEnabled { get; set; }

        public string ParentLabelId { get; set; }

        public SPOFileSensitivityLabelInfo(FileSensitivityLabelInfo label)
        {
            DisplayName = label.DisplayName;
            LabelId = label.LabelId;            
            ProtectionEnabled = label.ProtectionEnabled;
            ParentLabelId = label.ParentLabelId;            
        }
    }
}
