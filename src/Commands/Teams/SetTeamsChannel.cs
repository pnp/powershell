using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsChannel")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
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

        [Parameter(Mandatory = false)]
        public bool AllowNewMessageFromBots;

        [Parameter(Mandatory = false)]
        public bool AllowNewMessageFromConnectors;

        [Parameter(Mandatory = false)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<Enums.TeamChannelModerationSettingReplyRestriction>))]
        public Enums.TeamChannelModerationSettingReplyRestriction ReplyRestriction;

        [Parameter(Mandatory = false)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<Enums.TeamChannelModerationSettingNewMessageRestriction>))]
        public Enums.TeamChannelModerationSettingNewMessageRestriction UserNewMessageRestriction;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(this, Connection, AccessToken) ?? throw new PSArgumentException("Group not found");
            var teamChannel = Identity.GetChannel(this, Connection, AccessToken, groupId) ?? throw new PSArgumentException("Channel not found");

            // Flag to indicate if we have to use the beta endpoint to perform the update
            var betaRequired = false;

            if (ParameterSpecified(nameof(DisplayName)) && teamChannel.DisplayName != DisplayName)
            {
                teamChannel.DisplayName = DisplayName;
            }
            else
            {
                teamChannel.DisplayName = null;
            }

            if (ParameterSpecified(nameof(Description)) && teamChannel.Description != Description)
            {
                teamChannel.Description = Description;
            }
            else
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

            if (ParameterSpecified(nameof(AllowNewMessageFromBots)))
            {
                teamChannel.ModerationSettings.AllowNewMessageFromBots = AllowNewMessageFromBots;
                betaRequired = true;
            }

            if (ParameterSpecified(nameof(AllowNewMessageFromConnectors)))
            {
                teamChannel.ModerationSettings.AllowNewMessageFromConnectors = AllowNewMessageFromConnectors;
                betaRequired = true;
            }

            if (ParameterSpecified(nameof(ReplyRestriction)))
            {
                teamChannel.ModerationSettings.ReplyRestriction = ReplyRestriction;
                betaRequired = true;
            }

            if (ParameterSpecified(nameof(UserNewMessageRestriction)))
            {
                teamChannel.ModerationSettings.UserNewMessageRestriction = UserNewMessageRestriction;
                betaRequired = true;
            }

            teamChannel.MembershipType = null;
            try
            {
                var updated = TeamsUtility.UpdateChannel(this, Connection, AccessToken, groupId, teamChannel.Id, teamChannel, useBeta: betaRequired);
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
    }
}