using System.Management.Automation;
using PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileCheckedOut")]
    public class SetFileCheckedOut : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        protected override void ExecuteCmdlet()
        {
            // Use the Url as provided when a file exists there, only fall back to its decoded form when it does not.
            var serverRelativeUrl = Utilities.FileUrlResolver.Resolve(Url, null, Connection.PnPContext);

            IFile file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl);
            file.Checkout();
        }
    }
}
