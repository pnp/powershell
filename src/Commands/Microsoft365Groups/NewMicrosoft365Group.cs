using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.New, "PnPMicrosoft365Group", DefaultParameterSetName = ParameterSet_AssignedMembers)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class NewPnPMicrosoft365Group : PnPGraphCmdlet
    {
        private const string ParameterSet_AssignedMembers = "Assigned membership";
        private const string ParameterSet_DynamicMembers = "Dynamic membership";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DynamicMembers)]
        public string DisplayName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DynamicMembers)]
        public string Description;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DynamicMembers)]
        public string MailNickname;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        public bool MailEnabled = true;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public string[] Owners;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        public string[] Members;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<Framework.Enums.Office365Geography>))]
        public string PreferredDataLocation;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public string PreferredLanguage;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public SwitchParameter IsPrivate;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        [Alias("GroupLogoPath")]
        public string LogoPath;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public SwitchParameter CreateTeam;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public bool? HideFromAddressLists;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public bool? HideFromOutlookClients;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        [ArgumentCompleter(typeof(EnumAsStringArgumentCompleter<Enums.TeamResourceBehaviorOptions>))]
        public Enums.TeamResourceBehaviorOptions?[] ResourceBehaviorOptions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public Guid[] SensitivityLabels;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AssignedMembers)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public SwitchParameter SecurityEnabled;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DynamicMembers)]
        public string DynamicMembershipRule { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DynamicMembers)]
        public Enums.DynamicMembershipRuleProcessingState DynamicMembershipRuleProcessingState { get; set; } = Enums.DynamicMembershipRuleProcessingState.On;

        protected override void ExecuteCmdlet()
        {
            if (MailNickname.Contains(" "))
            {
                throw new ArgumentException("MailNickname cannot contain spaces.");
            }
            bool forceCreation;

            if (!Force)
            {
                var candidate = Microsoft365GroupsUtility.GetGroup(GraphRequestHelper, MailNickname, false, false, false, false);
                forceCreation = candidate == null || ShouldContinue($"The Microsoft 365 Group '{MailNickname} already exists. Do you want to create a new one?", Properties.Resources.Confirm);
            }
            else
            {
                forceCreation = true;
            }

            if (forceCreation)
            {
                if (ParameterSpecified(nameof(LogoPath)))
                {
                    if (System.IO.Path.IsPathRooted(LogoPath))
                    {
                        LogoPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LogoPath);
                    }
                    if (!System.IO.File.Exists(LogoPath))
                    {
                        throw new PSArgumentException("File specified for logo does not exist.");
                    }
                }
                var newGroup = new Microsoft365Group
                {
                    DisplayName = DisplayName,
                    Description = Description,
                    MailNickname = MailNickname,
                    Visibility = IsPrivate ? "Private" : "Public",
                    MailEnabled = MailEnabled,
                    SecurityEnabled = SecurityEnabled,
                    GroupTypes = new string[] { "Unified" },
                    PreferredDataLocation = PreferredDataLocation,
                    PreferredLanguage = PreferredLanguage,
                    MembershipRule = DynamicMembershipRule
                };

                if (!string.IsNullOrEmpty(DynamicMembershipRule))
                {
                    newGroup.MembershipRule = DynamicMembershipRule;
                    newGroup.MembershipRuleProcessingState = DynamicMembershipRuleProcessingState.ToString();
                    newGroup.GroupTypes = new string[] { "Unified", "DynamicMembership" };
                }

                if (ResourceBehaviorOptions != null && ResourceBehaviorOptions.Length > 0)
                {
                    var teamResourceBehaviorOptionsValue = new List<string>();
                    for (int i = 0; i < ResourceBehaviorOptions.Length; i++)
                    {
                        teamResourceBehaviorOptionsValue.Add(ResourceBehaviorOptions[i].ToString());
                    }
                    newGroup.ResourceBehaviorOptions = teamResourceBehaviorOptionsValue.ToArray();
                }

                var Labels = new List<string>();
                var contextSettings = Connection.Context.GetContextSettings();
                if (SensitivityLabels != null && SensitivityLabels.Length > 0)
                {
                    if (contextSettings.Type != Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                    {
                        foreach (var label in SensitivityLabels)
                        {
                            if (!Guid.Empty.Equals(label))
                            {
                                Labels.Add(label.ToString());
                            }
                        }
                    }
                    else
                    {
                        LogWarning("Adding sensitivity labels in App-only context is not supported by Graph API, so it will be skipped in Group creation");
                    }
                }

                var group = Microsoft365GroupsUtility.Create(GraphRequestHelper, newGroup, CreateTeam, LogoPath, Owners, Members, HideFromAddressLists, HideFromOutlookClients, Labels);

                var updatedGroup = Microsoft365GroupsUtility.GetGroup(GraphRequestHelper, group.Id.Value, true, false, false, true);

                WriteObject(updatedGroup);
            }
        }
    }
}