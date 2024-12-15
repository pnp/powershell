using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsChannel")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class RemoveTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("Removing the channel will also remove all the messages in the channel.", Properties.Resources.Confirm))
            {
                var groupId = Team.GetGroupId(RequestHelper);
                if (groupId != null)
                {
                    var channel = Identity.GetChannel(RequestHelper, groupId);
                    if (channel != null)
                    {
                        var response = TeamsUtility.DeleteChannel(RequestHelper, groupId, channel.Id);
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
                                WriteError(new ErrorRecord(new Exception($"Channel remove failed"), "REMOVEFAILED", ErrorCategory.InvalidResult, this));
                            }
                        }
                    }
                    else
                    {
                        throw new PSArgumentException("Channel not found");
                    }
                }
                else
                {
                    throw new PSArgumentException("Team not found");
                }
            }
        }
    }
}