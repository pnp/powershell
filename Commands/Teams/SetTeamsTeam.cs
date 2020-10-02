using PnP.Framework.Provisioning.Model.Teams;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Set, "TeamsTeam")]
    [MicrosoftGraphApiPermissionCheckAttribute(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class SetTeamsTeam : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public TeamVisibility Visibility;

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
        protected override void ExecuteCmdlet()
        {
            var groupId = Identity.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                try
                {
                    var team = TeamsUtility.GetTeamAsync(AccessToken, HttpClient, groupId).GetAwaiter().GetResult();
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
                        if((GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString()) != team.Visibility)
                        {
                            group.Visibility = (GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString());
                            updateGroup = true;
                        }
                        team.IsArchived = null; // cannot update this value;

                        if(updateGroup)
                        {
                            TeamsUtility.UpdateGroupAsync(HttpClient, AccessToken, groupId, group).GetAwaiter().GetResult();
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

                        var updated = TeamsUtility.UpdateTeamAsync(HttpClient, AccessToken, groupId, teamCI.ToTeam()).GetAwaiter().GetResult();
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
                        throw ex;
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