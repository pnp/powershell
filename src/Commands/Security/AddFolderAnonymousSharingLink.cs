using PnP.Core.Model.Security;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFolderAnonymousSharingLink")]
    [OutputType(typeof(FolderSharingLinkResult))]
    public class AddFolderAnonymousSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false)]
        public ShareType ShareType = ShareType.View;

        [Parameter(Mandatory = false)]
        public string Password;

        [Parameter(Mandatory = false)]
        public DateTime ExpirationDateTime;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            IFolder folder = Folder.GetFolder(ctx);            

            var shareLinkRequestOptions = new AnonymousLinkOptions()
            {
                Type = ShareType
            };

            if (ParameterSpecified(nameof(Password)))
            {
                shareLinkRequestOptions.Password = Password;
            }

            if (ParameterSpecified(nameof(ExpirationDateTime)))
            {
                shareLinkRequestOptions.ExpirationDateTime = ExpirationDateTime;
            }

            var sharedAnonymousFolder = folder.CreateAnonymousSharingLink(shareLinkRequestOptions);

            FolderSharingLinkResult folderAnonymousSharingLinkResult = new()
            {
                Id = sharedAnonymousFolder.Id,
                Link = sharedAnonymousFolder.Link,
                Roles = sharedAnonymousFolder.Roles,
                WebUrl = sharedAnonymousFolder.Link?.WebUrl
            };

            WriteObject(folderAnonymousSharingLinkResult);

        }
    }
}
