using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPList")]
    [OutputType(typeof(List))]
    public class SetList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind Identity;

        [Parameter(Mandatory = false)]
        public bool
            EnableContentTypes;

        [Parameter(Mandatory = false)]
        public
            SwitchParameter BreakRoleInheritance;

        [Parameter(Mandatory = false)]
        public
            SwitchParameter ResetRoleInheritance;

        [Parameter(Mandatory = false)]
        public
            SwitchParameter CopyRoleAssignments;

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
        public ListReadSecurity ReadSecurity;

        [Parameter(Mandatory = false)]
        public ListWriteSecurity WriteSecurity;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoCrawl;

        [Parameter(Mandatory = false)]
        public bool ExemptFromBlockDownloadOfNonViewableFiles;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);

            if (list != null)
            {
                list.EnsureProperties(l => l.EnableAttachments, l => l.EnableVersioning, l => l.EnableMinorVersions, l => l.Hidden, l => l.EnableModeration, l => l.BaseType, l => l.HasUniqueRoleAssignments, l => l.ContentTypesEnabled, l => l.ExemptFromBlockDownloadOfNonViewableFiles);

                var enableVersioning = list.EnableVersioning;
                var enableMinorVersions = list.EnableMinorVersions;
                var hidden = list.Hidden;
                var enableAttachments = list.EnableAttachments;

                var updateRequired = false;
                if (BreakRoleInheritance)
                {
                    list.BreakRoleInheritance(CopyRoleAssignments, ClearSubscopes);
                    updateRequired = true;
                }

                if ((list.HasUniqueRoleAssignments) && (ResetRoleInheritance))
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

                        if (ParameterSpecified(nameof(MajorVersions)))
                        {
                            list.MajorVersionLimit = (int)MajorVersions;
                            updateRequired = true;
                        }

                        if (ParameterSpecified(nameof(MinorVersions)) && list.EnableMinorVersions)
                        {
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
}
