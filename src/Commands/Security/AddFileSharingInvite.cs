using PnP.Core.Model.Security;
using PnP.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Add, "PnPFileSharingInvite")]
    [OutputType(typeof(void))]
    public class AddFileSharingInvite : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string FileUrl;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public string Message = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter RequireSignIn;

        [Parameter(Mandatory = false)]
        public SwitchParameter SendInvitation;

        [Parameter(Mandatory = false)]
        public PermissionRole Role = PermissionRole.Read;

        [Parameter(Mandatory = false)]
        public DateTime ExpirationDateTime;

        protected override void ExecuteCmdlet()
        {
            if (!RequireSignIn && !SendInvitation)
            {
                throw new ArgumentException("RequireSignIn and SendInvitation both cannot be false.");
            }

            if (Users?.Length > 1)
            {
                throw new ArgumentException("You can only invite one user at a time.");
            }

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
            foreach (var user in Users)
            {
                var driveRecipient = InviteOptions.CreateDriveRecipient(user);
                driveRecipients.Add(driveRecipient);
            }

            if (ParameterSpecified(nameof(Message)) && !string.IsNullOrEmpty(Message))
            {
                if (Message.Length > 2000)
                {
                    LogDebug("Invitation message length cannot exceed 2000 characters, trimming the message");
                    Message = Message.Substring(0, 2000);
                }
            }

            var shareRequestOptions = new InviteOptions()
            {
                Message = Message,
                RequireSignIn = RequireSignIn,
                SendInvitation = SendInvitation,
                Recipients = driveRecipients,
                Roles = new List<PermissionRole> { Role }
            };

            if (ParameterSpecified(nameof(ExpirationDateTime)))
            {
                shareRequestOptions.ExpirationDateTime = ExpirationDateTime;
            }

            var share = file.CreateSharingInvite(shareRequestOptions);
        }
    }
}
