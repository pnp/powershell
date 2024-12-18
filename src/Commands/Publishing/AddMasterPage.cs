using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Add, "PnPMasterPage")]
    public class AddMasterPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string SourceFilePath = string.Empty;

        [Parameter(Mandatory = true)]
        public string Title = string.Empty;

        [Parameter(Mandatory = true)]
        public string Description = string.Empty;

        [Parameter(Mandatory = false)]
        public string DestinationFolderHierarchy;

        [Parameter(Mandatory = false)]
        public string UIVersion = "15";

        [Parameter(Mandatory = false)]
        public string DefaultCssFile;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(SourceFilePath))
            {
                SourceFilePath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, SourceFilePath);
            }

            var masterPageFile = CurrentWeb.DeployMasterPage(SourceFilePath, Title, Description, UIVersion, DefaultCssFile, DestinationFolderHierarchy);
            WriteObject(masterPageFile);
        }
    }
}