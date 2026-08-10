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
            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            IFile file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(Url);

            file.Checkin(Comment, CheckinType);

            if (Approve)
            {
                file.Approve(Comment);
            }
        }
    }
}
