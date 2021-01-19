using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Add, "PnPGroupMember")]
    [Alias("Add-PnPUserToGroup")]
    [WriteAliasWarning("Please use Add-PnPGroupMember. The alias `Add-PnPUserToGroup` will be removed in the 1.5.0 release")]
    public class AddUserToGroup : PnPWebCmdlet
    {

        private const string ParameterSet_INTERNAL = "Internal";
        private const string ParameterSet_EXTERNAL = "External";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_INTERNAL)]
        public string LoginName;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_INTERNAL)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_EXTERNAL)]
        [Alias("Identity")]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_EXTERNAL)]
        public string EmailAddress;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_EXTERNAL)]
        public SwitchParameter SendEmail;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_EXTERNAL)]
        public string EmailBody = "Site shared with you.";

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_EXTERNAL)
            {
                var group = Group.GetGroup(CurrentWeb);
                group.InviteExternalUser(EmailAddress, SendEmail, EmailBody);
            }
            else
            {
                var group = Group.GetGroup(PnPContext);
                var user = PnPContext.Web.EnsureUser(LoginName);
                group.AddUser(user.LoginName);
            }
        }
    }
}
