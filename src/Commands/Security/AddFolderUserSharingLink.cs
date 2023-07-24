using PnP.Core.Model.Security;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFolderUserSharingLink")]
    [OutputType(typeof(FolderSharingLinkResult))]
    public class AddFolderUserSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public ShareType ShareType = ShareType.View;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            IFolder folder = Folder.GetFolder(ctx);

            // List of users to share the file/folder with
            var driveRecipients = new List<IDriveRecipient>();
            foreach (var user in Users)
            {
                var driveRecipient = UserLinkOptions.CreateDriveRecipient(user);
                driveRecipients.Add(driveRecipient);
            }

            var shareLinkRequestOptions = new UserLinkOptions()
            {
                Recipients = driveRecipients,
                Type = ShareType
            };

            var share = folder.CreateUserSharingLink(shareLinkRequestOptions);

            FolderSharingLinkResult folderUserSharingLinkResult = new()
            {
                Id = share.Id,
                Link = share.Link,
                Roles = share.Roles,
                WebUrl = share.Link?.WebUrl
            };

            WriteObject(folderUserSharingLinkResult);
        }
    }
}
