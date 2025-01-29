using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsTeam")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class RemoveTeamsTeam : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var groupId = Identity.GetGroupId(GraphRequestHelper);
            if (groupId != null)
            {
                if (Force || ShouldContinue("Removing the team will remove all messages in all channels in the team.", Properties.Resources.Confirm))
                {
                    var response = TeamsUtility.DeleteTeam(GraphRequestHelper, groupId);
                    if (!response.IsSuccessStatusCode)
                    {
                        if (GraphRequestHelper.TryGetGraphException(response, out GraphException ex))
                        {
                            if (ex.Error != null)
                            {
                                throw new PSInvalidOperationException(ex.Error.Message);
                            }
                        }
                        else
                        {
                            WriteError(new ErrorRecord(new Exception($"Team remove failed"), "REMOVEFAILED", ErrorCategory.InvalidResult, this));
                        }
                    }
                }
            }
            else
            {
                throw new PSArgumentException("Team not found");
            }
        }
    }
}