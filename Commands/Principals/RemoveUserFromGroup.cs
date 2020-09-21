using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPUserFromGroup")]
    public class RemoveUserFromGroup : PnPWebCmdlet
    {

        [Parameter(Mandatory = true)]
        [Alias("LogonName")]
        public string LoginName = string.Empty;

        [Parameter(Mandatory = true)]
        [Alias("GroupName")]
        public GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(SelectedWeb);
            try
            {
                User user = SelectedWeb.SiteUsers.GetByEmail(LoginName);
                ClientContext.Load(user);
                ClientContext.ExecuteQueryRetry();
                SelectedWeb.RemoveUserFromGroup(group, user);
            }
            catch
            {
                User user = SelectedWeb.SiteUsers.GetByLoginName(LoginName);
                ClientContext.Load(user);
                ClientContext.ExecuteQueryRetry();
                SelectedWeb.RemoveUserFromGroup(group, user);
            }
        }
    }
}
