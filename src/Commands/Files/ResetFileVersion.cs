using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Reset, "PnPFileVersion")]
    public class ResetFileVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ServerRelativeUrl = string.Empty;

        [Parameter(Mandatory = false)]
        public CheckinType CheckinType = CheckinType.MajorCheckIn;

        [Parameter(Mandatory = false)]
        public string CheckInComment = "Restored to previous version";

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.ResetFileToPreviousVersion(ServerRelativeUrl,CheckinType,CheckInComment);
        }
    }
}
