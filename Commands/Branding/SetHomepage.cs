using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "HomePage")]
    public class SetHomePage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string RootFolderRelativeUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.SetHomePage(RootFolderRelativeUrl);
        }
    }

}
