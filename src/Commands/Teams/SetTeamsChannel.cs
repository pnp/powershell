using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsChannel")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class SetTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsChannelPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public bool IsFavoriteByDefault;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(Connection, AccessToken);
            if (groupId != null)
            {
                var teamChannel = Identity.GetChannel(Connection, AccessToken, groupId);
                if (teamChannel != null)
                {
                    if (ParameterSpecified(nameof(DisplayName)) && teamChannel.DisplayName != DisplayName)
                    {
                        teamChannel.DisplayName = DisplayName;
                    } else
                    {
                        teamChannel.DisplayName = null;
                    }
                    if (ParameterSpecified(nameof(Description)) && teamChannel.Description != Description)
                    {
                        teamChannel.Description = Description;
                    } else
                    {
                        teamChannel.Description = null;
                    }

                    if (teamChannel.MembershipType.ToLower() == "standard" && ParameterSpecified(nameof(IsFavoriteByDefault)) && teamChannel.IsFavoriteByDefault != IsFavoriteByDefault)
                    {
                        teamChannel.IsFavoriteByDefault = IsFavoriteByDefault;
                    }
                    else
                    {
                        teamChannel.IsFavoriteByDefault = null;
                    }
                    teamChannel.MembershipType = null;
                    try 
                    {
                        var updated = TeamsUtility.UpdateChannelAsync(Connection, AccessToken, groupId, teamChannel.Id, teamChannel).GetAwaiter().GetResult();
                        WriteObject(updated);
                    }
                    catch (GraphException ex)
                    {
                        if (ex.Error != null)
                        {
                            throw new PSInvalidOperationException(ex.Error.Message);
                        }
                        else
                        {
                            throw;
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
                throw new PSArgumentException("Group not found");
            }

        }
    }
}