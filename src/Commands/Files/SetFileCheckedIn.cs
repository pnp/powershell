using System.Management.Automation;
using PnP.Core.Model.SharePoint;
using CheckinType = PnP.Core.Model.SharePoint.CheckinType;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileCheckedIn")]
    public class SetFileCheckedIn : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        [Parameter(Mandatory = false)]
        public CheckinType CheckinType = CheckinType.MajorCheckIn;

        [Parameter(Mandatory = false)]
        public string Comment = "";

        [Parameter(Mandatory = false)]
        public SwitchParameter Approve;

        protected override void ExecuteCmdlet()
        {
            // Use the Url as provided when a file exists there, only fall back to its decoded form when it does not.
            var serverRelativeUrl = Utilities.FileUrlResolver.Resolve(Url, null, Connection.PnPContext);

            IFile file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl);

            file.Checkin(Comment, CheckinType);

            if (Approve)
            {
                file.Approve(Comment);
            }
        }
    }
}
