using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPList")]
    [OutputType(typeof(List))]
    public class SetList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
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
        public string Path;

        [Parameter(Mandatory = false)]
        public bool EnableAutoExpirationVersionTrim;

        [Parameter(Mandatory = false)]
        public int ExpireVersionsAfterDays;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);

            if (list is null)
            {
                WriteWarning($"List {Identity} not found");
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

            list.EnsureProperties(l => l.EnableAttachments, l => l.EnableVersioning, l => l.EnableMinorVersions, l => l.Hidden, l => l.EnableModeration, l => l.BaseType, l => l.HasUniqueRoleAssignments, l => l.ContentTypesEnabled, l => l.ExemptFromBlockDownloadOfNonViewableFiles, l => l.DisableGridEditing);

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

            if (updateRequired)
            {
                list.Update();
                ClientContext.ExecuteQueryRetry();
            }

            updateRequired = false;

            if (list.EnableVersioning)
            {
                // list or doclib?
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
                                throw new PSArgumentException("You must specify a value for ExpireVersionsAfterDays and MajorVersions");
                            }

                            if (!ParameterSpecified(nameof(MinorVersions)) && list.EnableMinorVersions)
                            {
                                throw new PSArgumentException("You must specify a value for MinorVersions if it is enabled.");
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
                                throw new PSArgumentException("You must specify ExpireVersionsAfterDays to be 0 for NoExpiration or greater equal 30 for ExpireAfter.");
                            }
                        }

                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(ExpireVersionsAfterDays)) && (int)ExpireVersionsAfterDays >= 30)
                    {
                        if (list.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                        {
                            throw new PSArgumentException("The parameter ExpireVersionsAfterDays can't be set when AutoExpiration is enabled");
                        }

                        list.VersionPolicies.DefaultExpireAfterDays = (int)ExpireVersionsAfterDays;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(MajorVersions)))
                    {
                        if (list.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                        {
                            throw new PSArgumentException("The parameter MajorVersions can't be set when AutoExpiration is enabled");
                        }

                        list.MajorVersionLimit = (int)MajorVersions;
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(MinorVersions)) && list.EnableMinorVersions)
                    {
                        if (list.VersionPolicies.DefaultTrimMode == VersionPolicyTrimMode.AutoExpiration)
                        {
                            throw new PSArgumentException("The parameter MinorVersions can't be set when AutoExpiration is enabled");
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

            if (updateRequired)
            {
                list.Update();
                ClientContext.ExecuteQueryRetry();
            }

            WriteObject(list);
        }
    }
}