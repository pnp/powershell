using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPUserFromGroup")]
    public class RemoveUserFromGroup : PnPWebCmdlet
    {

        [Parameter(Mandatory = true)]
        public string LoginName = string.Empty;

        [Parameter(Mandatory = true)]
        public GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(CurrentWeb);
            try
            {
                User user = CurrentWeb.SiteUsers.GetByEmail(LoginName);
                ClientContext.Load(user);
                ClientContext.ExecuteQueryRetry();
                CurrentWeb.RemoveUserFromGroup(group, user);
            }
            catch
            {
                User user = CurrentWeb.SiteUsers.GetByLoginName(LoginName);
                ClientContext.Load(user);
                ClientContext.ExecuteQueryRetry();
                CurrentWeb.RemoveUserFromGroup(group, user);
            }
        }
    }
}
