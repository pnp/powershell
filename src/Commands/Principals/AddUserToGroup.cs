using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Add, "PnPUserToGroup")]
    public class AddUserToGroup : PnPWebCmdlet
    {

        [Parameter(Mandatory = true, ParameterSetName = "Internal")]
        public string LoginName;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Internal")]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "External")]
        public GroupPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = "External")]
        public string EmailAddress;

        [Parameter(Mandatory = false, ParameterSetName = "External")]
        public SwitchParameter SendEmail;

        [Parameter(Mandatory = false, ParameterSetName = "External")]
        public string EmailBody = "Site shared with you.";
     
        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(SelectedWeb);
            if (ParameterSetName == "External")
            {
                group.InviteExternalUser(EmailAddress, SendEmail, EmailBody);
            }
            else
            {
                SelectedWeb.AddUserToGroup(group, LoginName);
            }
        }
    }
}
