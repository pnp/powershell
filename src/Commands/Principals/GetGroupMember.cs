using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroupMember")]
    [OutputType(typeof(Microsoft.SharePoint.Client.User))]
    public class GetGroupMembers : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Identity")]
        public GroupPipeBind Group;

        [Parameter(Mandatory = false)]
        public string User;
        protected override void ExecuteCmdlet()
        {
            var group = Group.GetGroup(CurrentWeb);
            if (group == null)
            {
                throw new PSArgumentException("Group not found", nameof(Group));
            }

            if (ParameterSpecified(nameof(User)))
            {
                var user = group.Users.Where(u => u.LoginName == User || u.Email == User).FirstOrDefault();
                WriteObject(user);
            }
            else
            {
                WriteObject(group.Users, true);
            }
        }
    }
}