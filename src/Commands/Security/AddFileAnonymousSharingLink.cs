using PnP.Core.Model.Security;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFileAnonymousSharingLink")]
    [OutputType(typeof(FileSharingLinkResult))]
    public class AddFileAnonymousSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string FileUrl;

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

            if (!FileUrl.ToLower().StartsWith(ctx.Web.ServerRelativeUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(ctx.Web.ServerRelativeUrl, FileUrl);
            }
            else
            {
                serverRelativeUrl = FileUrl;
            }

            var file = ctx.Web.GetFileByServerRelativeUrl(serverRelativeUrl);

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

            var sharedAnonymousFile = file.CreateAnonymousSharingLink(shareLinkRequestOptions);

            FileSharingLinkResult fileAnonymousSharingLinkResult = new()
            {
                Id = sharedAnonymousFile.Id,
                Link = sharedAnonymousFile.Link,
                Roles = sharedAnonymousFile.Roles,
                WebUrl = sharedAnonymousFile.Link?.WebUrl
            };

            WriteObject(fileAnonymousSharingLinkResult);

        }
    }
}
