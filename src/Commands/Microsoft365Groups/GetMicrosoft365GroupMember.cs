using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupMember")]
    [Alias("Get-PnPMicrosoft365GroupMembers")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        [ValidateSet(new[] { "Member", "Guest" })]
        public string UserType;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(UserType)))
            {
                if (!string.IsNullOrEmpty(UserType) && UserType.ToLower() == "guest")
                {
                    var groupId = Identity.GetGroupId(HttpClient, AccessToken);
                    var guestUsers = TeamsUtility.GetUsersAsync(HttpClient, AccessToken, groupId.ToString(), UserType).GetAwaiter().GetResult();
                    WriteObject(guestUsers?.OrderBy(g => g.DisplayName), true);
                }
                else
                {
                    var members = Microsoft365GroupsUtility.GetMembersAsync(HttpClient, Identity.GetGroupId(HttpClient, AccessToken), AccessToken).GetAwaiter().GetResult();
                    WriteObject(members?.OrderBy(m => m.DisplayName), true);
                }
            }
            else
            {
                var members = Microsoft365GroupsUtility.GetMembersAsync(HttpClient, Identity.GetGroupId(HttpClient, AccessToken), AccessToken).GetAwaiter().GetResult();
                WriteObject(members?.OrderBy(m => m.DisplayName), true);
            }
        }
    }
}