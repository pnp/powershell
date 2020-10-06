using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "SitePolicy")]
    public class ApplySitePolicy : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

       
        protected override void ExecuteCmdlet()
        {
            SelectedWeb.ApplySitePolicy(Name);
        }
    }
}


