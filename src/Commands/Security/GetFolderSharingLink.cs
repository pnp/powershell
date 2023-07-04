using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderSharingLink")]
    public class GetFolderSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public FolderPipeBind Folder;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            IFolder folder = Folder.GetFolder(ctx);

            var sharingLinks = folder.GetShareLinks();

            WriteObject(sharingLinks?.RequestedItems, true);
        }
    }
}
