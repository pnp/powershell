using System.Management.Automation;
using PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Undo, "PnPFileCheckedOut")]
    public class UndoFileCheckedOut : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        protected override void ExecuteCmdlet()
        {
            // Use the Url as provided when a file exists there, only fall back to its decoded form when it does not.
            IFile file = Utilities.FileUrlResolver.ResolveFile(Url, null, ClientContext, CurrentWeb, Connection.PnPContext);
            file.UndoCheckout();

        }
    }
}
