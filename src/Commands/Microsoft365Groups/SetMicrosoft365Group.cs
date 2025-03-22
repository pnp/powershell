using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Set, "PnPMicrosoft365Group")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class SetMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public String[] Owners;

        [Parameter(Mandatory = false)]
        public String[] Members;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsPrivate;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [Alias("GroupLogoPath")]
        public string LogoPath;

        [Parameter(Mandatory = false)]
        public SwitchParameter CreateTeam;

        [Parameter(Mandatory = false)]
        public bool? HideFromAddressLists;

        [Parameter(Mandatory = false)]
        public bool? HideFromOutlookClients;

        [Parameter(Mandatory = false)]
        public Guid[] SensitivityLabels;

        [Parameter(Mandatory = false)]
        public string MailNickname;

        [Parameter(Mandatory = false)] // This is the name used in Microsoft Graph while the name RequireSenderAuthenticationEnabled is the one used within Exchange Online, but there its inversed, so we cannot easily add it as an alias here. They both are about the same feature.
        public bool? AllowExternalSenders;

        [Parameter(Mandatory = false)]
        public bool? AutoSubscribeNewMembers;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(GraphRequestHelper, false, false, false, false);

            if (group != null)
            {
                bool changed = false;
                bool exchangeOnlinePropertiesChanged = false;

                if (ParameterSpecified(nameof(DisplayName)))
                {
                    group.DisplayName = DisplayName;
                    changed = true;
                }
                if (ParameterSpecified(nameof(Description)))
                {
                    group.Description = Description;
                    changed = true;
                }
                if (ParameterSpecified(nameof(IsPrivate)))
                {
                    group.Visibility = IsPrivate ? "Private" : "Public";
                    changed = true;
                }
                if (ParameterSpecified(nameof(MailNickname)))
                {
                    //Ensures mailNickname contain only characters in the ASCII character set 0 - 127 except the following: @ () \ [] " ; : . <> , SPACE.
                    MailNickname = MailNickname.Replace("@", "").Replace("(", "").Replace(")", "").Replace("\\", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace(";", "").Replace(":", "").Replace(".", "").Replace("<", "").Replace(">", "").Replace(",", "").Replace(" ", "");
                    // Ensures Maximum length is 64 characters. 
                    if (MailNickname.Length > 64)
                    {
                        MailNickname = MailNickname.Substring(0, 64);
                    }
                    group.MailNickname = MailNickname;
                    changed = true;
                }
                if (changed)
                {
                    LogDebug("Updating Microsoft 365 Group properties in Microsoft Graph");
                    group = Microsoft365GroupsUtility.Update(GraphRequestHelper, group);
                }

                if (ParameterSpecified(nameof(AllowExternalSenders)) && AllowExternalSenders.HasValue)
                {
                    if (TokenHandler.RetrieveTokenType(AccessToken) != Enums.IdType.Delegate)
                    {
                        LogWarning($"{nameof(AllowExternalSenders)} can only be used with a delegate token. You're currently connected through an application token.");
                    }

                    group.AllowExternalSenders = AllowExternalSenders.Value;
                    exchangeOnlinePropertiesChanged = true;
                }

                if (ParameterSpecified(nameof(AutoSubscribeNewMembers)) && AutoSubscribeNewMembers.HasValue)
                {
                    if (TokenHandler.RetrieveTokenType(AccessToken) != Enums.IdType.Delegate)
                    {
                        LogWarning($"{nameof(AllowExternalSenders)} can only be used with a delegate token. You're currently connected through an application token.");
                    }

                    group.AutoSubscribeNewMembers = AutoSubscribeNewMembers.Value;
                    exchangeOnlinePropertiesChanged = true;
                }

                if (exchangeOnlinePropertiesChanged)
                {
                    LogDebug("Updating Microsoft 365 Group Exchange Online properties through Microsoft Graph");
                    group = Microsoft365GroupsUtility.UpdateExchangeOnlineSetting(GraphRequestHelper, group.Id.Value, group);
                }

                if (ParameterSpecified(nameof(Owners)))
                {
                    Microsoft365GroupsUtility.UpdateOwners(GraphRequestHelper, group.Id.Value, Owners);
                }

                if (ParameterSpecified(nameof(Members)))
                {
                    Microsoft365GroupsUtility.UpdateMembersAsync(GraphRequestHelper, group.Id.Value, Members);
                }

                if (ParameterSpecified(nameof(LogoPath)))
                {
                    if (!Path.IsPathRooted(LogoPath))
                    {
                        LogoPath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LogoPath);
                    }
                    Microsoft365GroupsUtility.UploadLogoAsync(GraphRequestHelper, group.Id.Value, LogoPath);
                }

                if (ParameterSpecified(nameof(CreateTeam)))
                {
                    if (!group.ResourceProvisioningOptions.Contains("Team"))
                    {
                        Microsoft365GroupsUtility.CreateTeam(GraphRequestHelper, group.Id.Value);
                    }
                    else
                    {
                        LogWarning("There is already a provisioned Team for this group. Skipping Team creation.");
                    }
                }

                if (ParameterSpecified(nameof(HideFromAddressLists)) || ParameterSpecified(nameof(HideFromOutlookClients)))
                {
                    // For this scenario a separate call needs to be made
                    Microsoft365GroupsUtility.SetVisibility(GraphRequestHelper, group.Id.Value, HideFromAddressLists, HideFromOutlookClients);
                }

                var assignedLabels = new List<AssignedLabels>();
                if (SensitivityLabels != null && SensitivityLabels.Length > 0)
                {
                    var contextSettings = Connection.Context.GetContextSettings();
                    if (contextSettings.Type != Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                    {
                        foreach (var label in SensitivityLabels)
                        {
                            if (!Guid.Empty.Equals(label))
                            {
                                assignedLabels.Add(new AssignedLabels
                                {
                                    labelId = label.ToString()
                                });
                            }
                        }
                        Microsoft365GroupsUtility.SetSensitivityLabels(GraphRequestHelper, group.Id.Value, assignedLabels);
                    }
                    else
                    {
                        LogWarning("Adding sensitivity labels in App-only context is not supported by Graph API, so it will be skipped in Group creation");
                    }
                }
            }
        }
    }
}