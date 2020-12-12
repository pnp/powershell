using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileCheckedIn")]
    public class SetFileCheckedIn : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true)]
        public string Url = string.Empty;

        [Parameter(Mandatory = false)]
        public CheckinType CheckinType = CheckinType.MajorCheckIn;

        [Parameter(Mandatory = false)]
        public string Comment = "";

        [Parameter(Mandatory = false)]
        public SwitchParameter Approve;

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.CheckInFile(Url, CheckinType, Comment);
            if (Approve)
                SelectedWeb.ApproveFile(Url, Comment);
        }
    }
}
