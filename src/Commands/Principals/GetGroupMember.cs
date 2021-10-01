using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroupMember")]
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
                    WriteObject(g.Users.Where(u => u.LoginName == User || u.Mail == User).FirstOrDefault());
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