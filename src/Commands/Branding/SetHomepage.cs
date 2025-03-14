using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPHomePage")]
    public class SetHomePage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string RootFolderRelativeUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if(RootFolderRelativeUrl.StartsWith("/"))
            {
                LogDebug($"Removing leading / from {nameof(RootFolderRelativeUrl)}");
                RootFolderRelativeUrl = RootFolderRelativeUrl.TrimStart('/');
            }

            LogDebug($"Setting homepage to {RootFolderRelativeUrl}");
            CurrentWeb.SetHomePage(RootFolderRelativeUrl);
        }
    }
}