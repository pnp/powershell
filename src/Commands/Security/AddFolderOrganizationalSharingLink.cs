using PnP.Core.Model.Security;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFolderOrganizationalSharingLink")]
    public class AddFolderOrganizationalSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false)]
        public ShareType ShareType = ShareType.View;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            IFolder folder = Folder.GetFolder(ctx);

            var shareLinkRequestOptions = new OrganizationalLinkOptions()
            {
                Type = ShareType
            };

            var share = folder.CreateOrganizationalSharingLink(shareLinkRequestOptions);

            FolderOrganizationalSharingLinkResult folderOrganizationalSharingLinkResult = new()
            {
                Id = share.Id,
                Link = share.Link,
                Roles = share.Roles,
            };

            WriteObject(folderOrganizationalSharingLinkResult);
        }
    }
}
