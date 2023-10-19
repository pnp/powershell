using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Core;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPGroupMember")]
    [OutputType(typeof(void))]    
    public class RemoveUserFromGroup : PnPWebCmdlet
    {

        [Parameter(Mandatory = true)]
        public string LoginName = string.Empty;

        [Parameter(Mandatory = true)]
        [Alias("Identity")]
        public GroupPipeBind Group;

        protected override void ExecuteCmdlet()
        {
            var group = Group.GetGroup(PnPContext);

            if (group != null)
            {
                try
                {
                    var user = PnPContext.Web.EnsureUser(LoginName);
                    group.RemoveUser(user.Id);
                }
                catch (PnP.Core.SharePointRestServiceException ex)
                {
                    throw new PSInvalidOperationException((ex.Error as SharePointRestError).Message);
                }
            }
            else
            {
                throw new PSArgumentException("Group not found.");
            }
        }
    }
}