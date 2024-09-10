using PnP.Framework.Provisioning.Model.Teams;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsTeam")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
    public class SetTeamsTeam : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public GroupVisibility Visibility;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowAddRemoveApps;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowChannelMentions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowCreateUpdateChannels;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowCreateUpdateRemoveConnectors;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowCreateUpdateRemoveTabs;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowCustomMemes;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowDeleteChannels;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowGiphy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowGuestCreateUpdateChannels;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowGuestDeleteChannels;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowOwnerDeleteMessages;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowStickersAndMemes;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowTeamMentions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowUserDeleteMessages;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowUserEditMessages;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Model.Teams.TeamGiphyContentRating GiphyContentRating;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? ShowInTeamsSearchAndSuggestions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Classification;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowCreatePrivateChannels;
        protected override void ExecuteCmdlet()
        {
            var groupId = Identity.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                try
                {
                    var team = TeamsUtility.GetTeam(this, AccessToken, Connection, groupId);
                    var updateGroup = false;
                    var group = new Group();
                    if (team != null)
                    {
                        if (ParameterSpecified(nameof(DisplayName)) && team.DisplayName != DisplayName)
                        {
                            updateGroup = true;
                            group.DisplayName = DisplayName;
                        }
                        else
                        {
                            team.DisplayName = null;
                        }
                        if (ParameterSpecified(nameof(Description)) && team.Description != Description)
                        {
                            updateGroup = true;
                            group.Description = Description;
                        }
                        else
                        {
                            team.Description = null;
                        }
                        if (ParameterSpecified(nameof(Visibility)) && (GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString()) != team.Visibility)
                        {
                            group.Visibility = (GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString());
                            updateGroup = true;
                        }
                        else
                        {
                            group.Visibility = team.Visibility;
                        }
                        team.IsArchived = null; // cannot update this value;

                        if(updateGroup)
                        {
                            TeamsUtility.UpdateGroup(this, Connection, AccessToken, groupId, group);
                        }

                        var teamCI = new TeamCreationInformation();
                        teamCI.AllowAddRemoveApps = ParameterSpecified(nameof(AllowAddRemoveApps)) ? AllowAddRemoveApps : null;
                        teamCI.AllowChannelMentions = ParameterSpecified(nameof(AllowChannelMentions)) ? AllowChannelMentions : null;
                        teamCI.AllowCreateUpdateChannels = ParameterSpecified(nameof(AllowCreateUpdateChannels)) ? AllowCreateUpdateChannels : null;
                        teamCI.AllowCreateUpdateRemoveConnectors = ParameterSpecified(nameof(AllowCreateUpdateRemoveConnectors)) ? AllowCreateUpdateRemoveConnectors : null;
                        teamCI.AllowCreateUpdateRemoveTabs = ParameterSpecified(nameof(AllowCreateUpdateRemoveTabs)) ? AllowCreateUpdateRemoveTabs : null;
                        teamCI.AllowCustomMemes = ParameterSpecified(nameof(AllowCustomMemes)) ? AllowCustomMemes : null;
                        teamCI.AllowDeleteChannels = ParameterSpecified(nameof(AllowDeleteChannels)) ? AllowDeleteChannels : null;
                        teamCI.AllowGiphy = ParameterSpecified(nameof(AllowGiphy)) ? AllowGiphy : null;
                        teamCI.AllowGuestCreateUpdateChannels = ParameterSpecified(nameof(AllowGuestCreateUpdateChannels)) ? AllowGuestCreateUpdateChannels : null;
                        teamCI.AllowGuestDeleteChannels = ParameterSpecified(nameof(AllowGuestDeleteChannels)) ? AllowGuestDeleteChannels : null;
                        teamCI.AllowOwnerDeleteMessages = ParameterSpecified(nameof(AllowOwnerDeleteMessages)) ? AllowOwnerDeleteMessages : null;
                        teamCI.AllowStickersAndMemes = ParameterSpecified(nameof(AllowStickersAndMemes)) ? AllowStickersAndMemes : null;
                        teamCI.AllowTeamMentions = ParameterSpecified(nameof(AllowTeamMentions)) ? AllowTeamMentions : null;
                        teamCI.AllowUserDeleteMessages = ParameterSpecified(nameof(AllowUserDeleteMessages)) ? AllowUserDeleteMessages : null;
                        teamCI.AllowUserEditMessages = ParameterSpecified(nameof(AllowUserEditMessages)) ? AllowUserEditMessages : null;
                        teamCI.Classification = ParameterSpecified(nameof(Classification)) ? Classification : null;
                        teamCI.AllowCreatePrivateChannels = ParameterSpecified(nameof(AllowCreatePrivateChannels)) ? AllowCreatePrivateChannels : null;                        

                        var updated = TeamsUtility.UpdateTeam(this, Connection, AccessToken, groupId, teamCI.ToTeam(group.Visibility.Value));
                        WriteObject(updated);
                    }
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
                throw new PSArgumentException("Team not found");
            }

        }
    }
}