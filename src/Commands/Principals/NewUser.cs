using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.New, "PnPUser")]
    public class NewUser : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LoginName = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var user = SelectedWeb.EnsureUser(LoginName);
            ClientContext.Load(user, u => u.Email, u => u.Id, u => u.IsSiteAdmin, u => u.Groups, u => u.PrincipalType, u => u.Title, u => u.IsHiddenInUI, u => u.UserId, u => u.LoginName);
            ClientContext.ExecuteQueryRetry();
            WriteObject(user);
        }
    }
}
