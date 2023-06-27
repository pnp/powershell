using PnP.Core.Model.Security;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFileUserSharingLink")]
    [OutputType(typeof(FileSharingLinkResult))]
    public class AddPnPFileUserSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string FileUrl;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public ShareType ShareType = ShareType.View;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            if (!FileUrl.ToLower().StartsWith(ctx.Web.ServerRelativeUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(ctx.Web.ServerRelativeUrl, FileUrl);
            }
            else
            {
                serverRelativeUrl = FileUrl;
            }

            var file = ctx.Web.GetFileByServerRelativeUrl(serverRelativeUrl);

            // List of users to share the file/folder with
            var driveRecipients = new List<IDriveRecipient>();
            foreach(var user in Users)
            {
                var driveRecipient = UserLinkOptions.CreateDriveRecipient(user);
                driveRecipients.Add(driveRecipient);
            }

            var shareLinkRequestOptions = new UserLinkOptions()
            {
                // Users can see and edit the file online, but not download it
                Type = ShareType,
                Recipients = driveRecipients
            };

            var share = file.CreateUserSharingLink(shareLinkRequestOptions);
            
            FileSharingLinkResult fileUserSharingLinkResult = new()
            {
                Id = share.Id,
                Link = share.Link,
                Roles = share.Roles,
                WebUrl = share.Link?.WebUrl
            };

            WriteObject(fileUserSharingLinkResult);
        }
    }
}
