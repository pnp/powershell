using PnP.Core.Model.Security;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFileOrganizationalSharingLink")]
    public class AddFileOrganizationalSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string FileUrl;

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

            var shareLinkRequestOptions = new OrganizationalLinkOptions()
            {
                Type = ShareType
            };

            var share = file.CreateOrganizationalSharingLink(shareLinkRequestOptions);

            FileOrganizationalSharingLinkResult fileOrganizationalSharingLinkResult = new()
            {
                Id = share.Id,
                Link = share.Link,
                Roles = share.Roles,
            };

            WriteObject(fileOrganizationalSharingLinkResult);
        }
    }
}
