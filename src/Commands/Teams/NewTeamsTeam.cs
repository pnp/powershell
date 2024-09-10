using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Model.Teams;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.New, "PnPTeamsTeam")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
    public class NewTeamsTeam : PnPGraphCmdlet
    {
        private const string ParameterSet_EXISTINGGROUP = "For an existing group";
        private const string ParameterSet_NEWGROUP = "For a new group";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_EXISTINGGROUP)]
        public string GroupId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_NEWGROUP)]
        [ValidateLength(1, 256)]
        public string DisplayName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWGROUP)]
        public string MailNickName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWGROUP)]
        [ValidateLength(0, 1024)]
        public string Description;

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

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWGROUP)]
        public TeamVisibility Visibility;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? ShowInTeamsSearchAndSuggestions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Classification;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWGROUP)]
        public TeamsTemplateType Template = TeamsTemplateType.None;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public bool? AllowCreatePrivateChannels;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string[] Owners;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string[] Members;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWGROUP)]
        public TeamResourceBehaviorOptions?[] ResourceBehaviorOptions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWGROUP)]
        public Guid[] SensitivityLabels;

        protected override void ExecuteCmdlet()
        {
            var teamCI = new TeamCreationInformation()
            {
                AllowAddRemoveApps = AllowAddRemoveApps,
                AllowChannelMentions = AllowChannelMentions,
                AllowCreateUpdateChannels = AllowCreateUpdateChannels,
                AllowCreateUpdateRemoveConnectors = AllowCreateUpdateRemoveConnectors,
                AllowCreateUpdateRemoveTabs = AllowCreateUpdateRemoveTabs,
                AllowCustomMemes = AllowCustomMemes,
                AllowDeleteChannels = AllowDeleteChannels,
                AllowGiphy = AllowGiphy,
                AllowGuestCreateUpdateChannels = AllowGuestCreateUpdateChannels,
                AllowGuestDeleteChannels = AllowGuestDeleteChannels,
                AllowOwnerDeleteMessages = AllowOwnerDeleteMessages,
                AllowStickersAndMemes = AllowStickersAndMemes,
                AllowTeamMentions = AllowTeamMentions,
                AllowUserDeleteMessages = AllowUserDeleteMessages,
                AllowUserEditMessages = AllowUserEditMessages,
                Classification = Classification,
                Description = Description,
                DisplayName = DisplayName,
                GiphyContentRating = GiphyContentRating,
                GroupId = GroupId,
                ShowInTeamsSearchAndSuggestions = ShowInTeamsSearchAndSuggestions,
                Visibility = (GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString()),
                AllowCreatePrivateChannels = AllowCreatePrivateChannels
            };

            var contextSettings = Connection.Context.GetContextSettings();
            if (contextSettings.Type == Framework.Utilities.Context.ClientContextType.AzureADCertificate)
            {
                if (SensitivityLabels != null && SensitivityLabels.Length > 0)
                {
                    SensitivityLabels = null;
                    WriteWarning("Adding sensitivity labels in App-only context is not supported by Graph API, so it will be skipped in Team creation");
                }
            }

            WriteObject(TeamsUtility.NewTeam(this, AccessToken, Connection, GroupId, DisplayName, Description, Classification, MailNickName, (GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString()), teamCI, Owners, Members, SensitivityLabels, Template, ResourceBehaviorOptions));
        }
    }
}
