using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    public class ImportFlowResult
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public ImportFlowDetails Details { get; set; }
    }

    public class ImportFlowDetails
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
