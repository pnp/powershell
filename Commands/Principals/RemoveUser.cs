using System.Management.Automation;
using Microsoft.SharePoint.Client;

using System.Linq.Expressions;
using System;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPUser")]
    public class RemoveUser : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public UserPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter Confirm;

        protected override void ExecuteCmdlet()
        {
            var retrievalExpressions = new Expression<Func<User, object>>[]
            {
                u => u.Id,
                u => u.LoginName,
                u => u.Email
            };

            User user = null;
            if (Identity.User != null)
            {
                WriteVerbose($"Received user instance {Identity.Login}");
                user = Identity.User;
            }
            else if (Identity.Id > 0)
            {
                WriteVerbose($"Retrieving user by Id {Identity.Id}");
                user = ClientContext.Web.GetUserById(Identity.Id);
            }
            else if (!string.IsNullOrWhiteSpace(Identity.Login))
            {
                WriteVerbose($"Retrieving user by LoginName {Identity.Login}");
                user = ClientContext.Web.SiteUsers.GetByLoginName(Identity.Login);
            }
            if (ClientContext.HasPendingRequest)
            {
                ClientContext.Load(user, retrievalExpressions);
                ClientContext.ExecuteQueryRetry();
            }

            if (user != null)
            {
                if (Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Properties.Resources.RemoveUser, user.Id, user.LoginName, user.Email), Properties.Resources.Confirm))
                {
                    WriteVerbose($"Removing user {user.Id} {user.LoginName} {user.Email}");
                    ClientContext.Web.SiteUsers.Remove(user);
                    ClientContext.ExecuteQueryRetry();
                }
            }
            else
            {
                throw new ArgumentException("Unable to find user", "Identity");
            }
        }
    }
}