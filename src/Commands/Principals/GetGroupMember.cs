using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroupMember")]
    [Alias("Get-PnPGroupMembers")]
    public class GetGroupMembers : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Identity")]
        public GroupPipeBind Group;

        [Parameter(Mandatory = false)]
        public string User;
        protected override void ExecuteCmdlet()
        {

            if (ParameterSpecified(nameof(User)))
            {
                var g = Group.GetGroup(PnPContext);
                if (g != null)
                {
                    WriteObject(g.Users.GetFirstOrDefault(u => u.LoginName == User || u.Mail == User));
                }
                else
                {
                    throw new PSArgumentException("Group not found");
                }
            }
            else
            {
                var group = Group.GetGroup(CurrentWeb);
                if (group != null)
                {
                    WriteObject(group.Users, true);
                }
                else
                {
                    throw new PSArgumentException("Group not found");

                }
            }
        }
    }
}