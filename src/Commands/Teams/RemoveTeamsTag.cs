using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsTag")]
    [RequiredApiApplicationPermissions("graph/TeamworkTag.ReadWrite")]
    public class RemoveTeamsTag : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsTagPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId != null)
            {
                var tag = Identity.GetTag(RequestHelper, groupId);
                if (tag != null)
                {
                    if (Force || ShouldContinue("Do you want to remove this tag ?", Properties.Resources.Confirm))
                    {
                        var response = TeamsUtility.DeleteTag(RequestHelper, groupId, tag.Id);
                        if (!response.IsSuccessStatusCode)
                        {
                            if (RequestHelper.TryGetGraphException(response, out GraphException ex))
                            {
                                if (ex.Error != null)
                                {
                                    throw new PSInvalidOperationException(ex.Error.Message);
                                }
                            }
                            else
                            {
                                throw new PSInvalidOperationException("Tag remove failed");
                            }
                        }
                    }
                }
                else
                {
                    throw new PSArgumentException("Tag not found");
                }
            }
            else
            {
                throw new PSArgumentException("Team not found", nameof(Team));
            }
        }
    }
}