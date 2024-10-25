using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPUser")]
    [OutputType(typeof(void))]
    public class RemoveUser : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public UserPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var retrievalExpressions = new Expression<Func<User, object>>[]
            {
                u => u.Id,
                u => u.LoginName,
                u => u.Email
            };

            User user = Identity.GetUser(ClientContext);

            if (user != null)
            {
                if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveUser, user.Id, user.LoginName, user.Email), Properties.Resources.Confirm))
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