using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileCheckedOut")]
    public class SetFileCheckedOut : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true)]
        public string Url = string.Empty;

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.CheckOutFile(Url);
        }
    }
}
