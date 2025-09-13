using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPList")]
    [OutputType(typeof(List))]
    public class SetList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        [Parameter(Mandatory = false)]
        public bool EnableContentTypes;

        [Parameter(Mandatory = false)]
        public SwitchParameter BreakRoleInheritance;

        [Parameter(Mandatory = false)]
        public SwitchParameter ResetRoleInheritance;

        [Parameter(Mandatory = false)]
        public SwitchParameter CopyRoleAssignments;

        [Parameter(Mandatory = false)]
        public SwitchParameter ClearSubscopes;

        [Parameter(Mandatory = false)]
        public string Title = string.Empty;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public bool Hidden;

        [Parameter(Mandatory = false)]
        public bool AllowDeletion;

        [Parameter(Mandatory = false)]
        public bool ForceCheckout;

        [Parameter(Mandatory = false)]
        public ListExperience ListExperience;

        [Parameter(Mandatory = false)]
        public bool EnableAttachments;

        [Parameter(Mandatory = false)]
        public bool EnableFolderCreation;

        [Parameter(Mandatory = false)]
        public bool EnableVersioning;

        [Parameter(Mandatory = false)]
        public bool EnableMinorVersions;

        [Parameter(Mandatory = false)]
        public uint MajorVersions = 10;

        [Parameter(Mandatory = false)]
        public uint MinorVersions = 10;

        [Parameter(Mandatory = false)]
        public bool EnableModeration;

        [Parameter(Mandatory = false)]
        public DraftVisibilityType DraftVersionVisibility;

        [Parameter(Mandatory = false)]
        public ListReadSecurity ReadSecurity;

        [Parameter(Mandatory = false)]
        public ListWriteSecurity WriteSecurity;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoCrawl;

        [Parameter(Mandatory = false)]
        public bool ExemptFromBlockDownloadOfNonViewableFiles;

        [Parameter(Mandatory = false)]
        public bool DisableGridEditing;

        [Parameter(Mandatory = false)]
        public bool DisableCommenting;

        [Parameter(Mandatory = false)]
        public string Path;

        [Parameter(Mandatory = false)]
        public bool EnableAutoExpirationVersionTrim;

        [Parameter(Mandatory = false)]
        public int ExpireVersionsAfterDays;

        [Parameter(Mandatory = false)]
        public SensitivityLabelPipeBind DefaultSensitivityLabelForLibrary;

        [Parameter(Mandatory = false)]
        public DocumentLibraryOpenDocumentsInMode OpenDocumentsMode;

        [Parameter(Mandatory = false)]
        public bool EnableClassicAudienceTargeting;

        [Parameter(Mandatory = false)]
        public bool EnableModernAudienceTargeting;

        [Parameter(Mandatory = false)]
        public ListColor Color;

        [Parameter(Mandatory = false)]
        public ListIcon Icon;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);

            if (list is null)
            {
                LogWarning($"List {Identity} not found");
                return;
            }

            if (ParameterSpecified(nameof(Path)))
            {
                // Move the list to its newly requested location within the same site
                list.RootFolder.MoveTo(Path);
                ClientContext.ExecuteQueryRetry();

                // Fetch the list again so it will have its updated location and can be used for property updates
                var newIdentity = new ListPipeBind(list.Id);
                list = newIdentity.GetList(CurrentWeb);
            }

            list.EnsureProperties(l => l.EnableAttachments, l => l.EnableVersioning, l => l.EnableMinorVersions, l => l.Hidden, l => l.AllowDeletion, l => l.EnableModeration, l => l.BaseType, l => l.HasUniqueRoleAssignments, l => l.ContentTypesEnabled, l => l.ExemptFromBlockDownloadOfNonViewableFiles, l => l.DisableGridEditing, l => l.DisableCommenting);

            var enableVersioning = list.EnableVersioning;
            var enableMinorVersions = list.EnableMinorVersions;
            var enableAttachments = list.EnableAttachments;
            var updateRequired = false;
            if (BreakRoleInheritance)
            {
                list.BreakRoleInheritance(CopyRoleAssignments, ClearSubscopes);
                updateRequired = true;
            }

            if (list.HasUniqueRoleAssignments && ResetRoleInheritance)
            {
                list.ResetRoleInheritance();
                updateRequired = true;
            }

            if (!string.IsNullOrEmpty(Title))
            {
                list.Title = Title;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(Hidden)) && Hidden != list.Hidden)
            {
                list.Hidden = Hidden;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(AllowDeletion)) && AllowDeletion != list.AllowDeletion)
            {
                list.AllowDeletion = AllowDeletion;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnableContentTypes)) && list.ContentTypesEnabled != EnableContentTypes)
            {
                list.ContentTypesEnabled = EnableContentTypes;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnableVersioning)) && EnableVersioning != enableVersioning)
            {
                list.EnableVersioning = EnableVersioning;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnableMinorVersions)) && EnableMinorVersions != enableMinorVersions)
            {
                list.EnableMinorVersions = EnableMinorVersions;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnableModeration)) && list.EnableModeration != EnableModeration)
            {
                list.EnableModeration = EnableModeration;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DraftVersionVisibility)))
            {
                list.DraftVersionVisibility = DraftVersionVisibility;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnableAttachments)) && EnableAttachments != enableAttachments)
            {
                list.EnableAttachments = EnableAttachments;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(Description)))
            {
                list.Description = Description;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnableFolderCreation)))
            {
                list.EnableFolderCreation = EnableFolderCreation;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(ForceCheckout)))
            {
                list.ForceCheckout = ForceCheckout;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(ListExperience)))
            {
                list.ListExperienceOptions = ListExperience;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(ReadSecurity)))
            {
                list.ReadSecurity = (int)ReadSecurity;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(WriteSecurity)))
            {
                list.WriteSecurity = (int)WriteSecurity;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(NoCrawl)))
            {
                list.NoCrawl = NoCrawl;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(ExemptFromBlockDownloadOfNonViewableFiles)))
            {
                list.SetExemptFromBlockDownloadOfNonViewableFiles(ExemptFromBlockDownloadOfNonViewableFiles);
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DisableGridEditing)))
            {
                list.DisableGridEditing = DisableGridEditing;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DisableCommenting)))
            {
                list.DisableCommenting = DisableCommenting;
                updateRequired = true;
            }


            if (ParameterSpecified(nameof(Color))) {
                list.Color = ((int)Color).ToString();
                updateRequired = true;
            }

            if(ParameterSpecified(nameof(Icon))) 
            {
                list.Icon = ((int)Icon).ToString();
                updateRequired = true;
            }

            if (updateRequired)
            {
                list.Update();
                ClientContext.ExecuteQueryRetry();
            }

            if (ParameterSpecified(nameof(EnableClassicAudienceTargeting)))
            {
                list.EnableClassicAudienceTargeting();
            }

            if (ParameterSpecified(nameof(EnableModernAudienceTargeting)))
            {
                list.EnableModernAudienceTargeting();
            }

            updateRequired = false;

            if (list.EnableVersioning)
            {
                // Is this for a list or a document library
                if (list.BaseType == BaseType.DocumentLibrary)
                {
                    list.EnsureProperties(l => l.VersionPolicies);

                    if (ParameterSpecified(nameof(EnableAutoExpirationVersionTrim)))
                    {
                        if (EnableAutoExpirationVersionTrim)
                        {
                            list.VersionPolicies.DefaultTrimMode = VersionPolicyTrimMode.AutoExpiration;
                        }
                        else
                        {
                            if (!ParameterSpecified(nameof(MajorVersions)) || !ParameterSpecified(nameof(ExpireVersionsAfterDays)))
                            {
                                throw new PSArgumentException($"You must specify a value for {nameof(ExpireVersionsAfterDays)} and {nameof(MajorVersions)}", nameof(ExpireVersionsAfterDays));
                            }

                            if (!ParameterSpecified(nameof(MinorVersions)) && list.EnableMinorVersions)
                            {
                                throw new PSArgumentException($"You must specify a value for {nameof(MinorVersions)} if it is enabled.", nameof(MinorVersions));
                            }

                            if (ExpireVersionsAfterDays == 0)
                            {
                                list.VersionPolicies.DefaultTrimMode = VersionPolicyTrimMode.NoExpiration;
                            }
                            else if (ExpireVersionsAfterDays >= 30)
                            {
                                list.VersionPolicies.DefaultTrimMode = VersionPolicyTrimMode.ExpireAfter;
                            }
                            else
                            {
                                throw new PSArgumentException($"You must specify {nameof(ExpireVersionsAfterDays)} to be 0 for NoExpiration or greater equal 30 for ExpireAfter.", nameof(ExpireVersionsAfterDays));
                            }
                        }

                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(ExpireVersionsAfterDays)) && (int)ExpireVersionsAfterDays >= 30)
                    {
                        if (list.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                        {
                            throw new PSArgumentException($"The parameter {nameof(ExpireVersionsAfterDays)} can't be set when AutoExpiration is enabled");
                        }

                        list.VersionPolicies.DefaultExpireAfterDays = (int)ExpireVersionsAfterDays;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(MajorVersions)))
                    {
                        if (list.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                        {
                            throw new PSArgumentException($"The parameter {nameof(MajorVersions)} can't be set when AutoExpiration is enabled", nameof(MajorVersions));
                        }

                        list.MajorVersionLimit = (int)MajorVersions;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(MinorVersions)) && list.EnableMinorVersions)
                    {
                        if (list.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                        {
                            throw new PSArgumentException($"The parameter {nameof(MinorVersions)} can't be set when AutoExpiration is enabled", nameof(MinorVersions));
                        }

                        list.MajorWithMinorVersionsLimit = (int)MinorVersions;
                        updateRequired = true;
                    }
                }
                else
                {
                    if (ParameterSpecified(nameof(MajorVersions)))
                    {
                        list.MajorVersionLimit = (int)MajorVersions;
                        updateRequired = true;
                    }
                }
            }

            if (ParameterSpecified(nameof(DefaultSensitivityLabelForLibrary)))
            {
                if (DefaultSensitivityLabelForLibrary == null)
                {
                    LogDebug("Removing sensitivity label from library");
                    list.DefaultSensitivityLabelForLibrary = null;
                    updateRequired = true;
                }
                else
                {
                    if (DefaultSensitivityLabelForLibrary.LabelId.HasValue)
                    {
                        LogDebug($"Setting provided sensitivity label id '{DefaultSensitivityLabelForLibrary.LabelId}' as the default sensitivity label for the library");
                        list.DefaultSensitivityLabelForLibrary = DefaultSensitivityLabelForLibrary.LabelId.ToString();
                        updateRequired = true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(DefaultSensitivityLabelForLibrary.LabelName))
                        {
                            LogDebug($"Looking up sensitivity label id by label name '{DefaultSensitivityLabelForLibrary.LabelName}'");
                            var label = DefaultSensitivityLabelForLibrary.GetLabelByNameThroughGraph(Connection, GraphRequestHelper);

                            if (label == null || !label.Id.HasValue)
                            {
                                throw new ArgumentException($"Unable to find a sensitivity label with the provided name '{DefaultSensitivityLabelForLibrary.LabelName}'", nameof(DefaultSensitivityLabelForLibrary));
                            }
                            else
                            {
                                LogDebug($"Provided sensitivity label name '{DefaultSensitivityLabelForLibrary.LabelName}' resolved to sensitivity label id '{label.Id.Value}' and will be set as the default sensitivity label for the library");
                                list.DefaultSensitivityLabelForLibrary = label.Id.Value.ToString();
                                updateRequired = true;
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Unable set the default sensitivity label for the library as there's no label name or label Id", nameof(DefaultSensitivityLabelForLibrary));
                        }
                    }
                }
            }

            if (ParameterSpecified(nameof(OpenDocumentsMode)))
            {
                // Is this for a list or a document library
                if (list.BaseType == BaseType.DocumentLibrary)
                {
                    LogDebug($"Configuring document library to use default open mode to be '{OpenDocumentsMode}'");

                    switch (OpenDocumentsMode)
                    {
                        case DocumentLibraryOpenDocumentsInMode.Browser:
                            list.DefaultItemOpenInBrowser = true;
                            break;

                        case DocumentLibraryOpenDocumentsInMode.ClientApplication:
                            list.DefaultItemOpenInBrowser = false;
                            break;
                    }
                    updateRequired = true;
                }
                else
                {
                    LogWarning($"{nameof(OpenDocumentsMode)} is only supported for document libraries");
                }

                switch (OpenDocumentsMode)
                {
                    case DocumentLibraryOpenDocumentsInMode.Browser:
                        list.DefaultItemOpenInBrowser = true;
                        break;

                    case DocumentLibraryOpenDocumentsInMode.ClientApplication:
                        list.DefaultItemOpenInBrowser = false;
                        break;
                }
                updateRequired = true;
            }

            if (updateRequired)
            {
                list.Update();
                ClientContext.ExecuteQueryRetry();
            }

            WriteObject(list);
        }
    }
}