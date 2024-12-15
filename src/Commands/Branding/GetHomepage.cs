using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPHomePage")]
    public class GetHomePage : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var folder = CurrentWeb.RootFolder;

            ClientContext.Load(folder, f => f.WelcomePage);

            ClientContext.ExecuteQueryRetry();

            if (string.IsNullOrEmpty(folder.WelcomePage))
            {
                WriteObject("default.aspx");
            }
            else
            {
                WriteObject(folder.WelcomePage);
            }
        }
    }
}
