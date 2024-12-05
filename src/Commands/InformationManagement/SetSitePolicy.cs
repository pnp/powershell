using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPSitePolicy")]
    public class ApplySitePolicy : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

       
        protected override void ExecuteCmdlet()
        {
            CurrentWeb.ApplySitePolicy(Name);
        }
    }
}


