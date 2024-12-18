using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPAlert")]
    [OutputType(typeof(void))]
    public class RemoveAlert : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public UserPipeBind User;

        [Parameter(Mandatory = true)]
        public AlertPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            User user;
            if (null != User)
            {
                user = User.GetUser(ClientContext);
                if (user == null)
                {
                    throw new ArgumentException("Unable to find user", "Identity");
                }
            }
            else
            {
                user = CurrentWeb.CurrentUser;
            }
            if (!Force)
            {
                user.EnsureProperty(u => u.LoginName);
            }
            if (Force || ShouldContinue($"Remove alert {Identity.Id} for {user.LoginName}?", Properties.Resources.Confirm))
            {
                user.Alerts.DeleteAlert(Identity.Id);
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}