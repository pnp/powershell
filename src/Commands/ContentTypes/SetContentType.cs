using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Set, "PnPContentType", DefaultParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
    public class SetContentType : PnPWebCmdlet
    {
        private const string ParameterSet_FormCustomizersConvenienceParams = "Form Customizers Convenience Options";
        private const string ParameterSet_FormCustomizersParams = "Form Customizers Options";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FormCustomizersParams)]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_FormCustomizersParams)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public SwitchParameter InSiteHierarchy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public SwitchParameter UpdateChildren;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string Name;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string Description;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string Group;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public bool Hidden;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public bool ReadOnly;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public bool Sealed;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        public string FormClientSideComponentId;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersConvenienceParams)]
        public string FormClientSideComponentProperties;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string DisplayFormClientSideComponentId;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string DisplayFormClientSideComponentProperties;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string NewFormClientSideComponentId;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string NewFormClientSideComponentProperties;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string EditFormClientSideComponentId;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_FormCustomizersParams)]
        public string EditFormClientSideComponentProperties;

        protected override void ExecuteCmdlet()
        {
            ContentType ct = null;
            List list = null;
            if (List != null)
            {
                list = List?.GetListOrThrow(nameof(List), CurrentWeb);

                ct = Identity.GetContentTypeOrError(this, nameof(Identity), list);
            }
            else
            {
                ct = Identity?.GetContentTypeOrThrow(nameof(Identity), CurrentWeb, InSiteHierarchy);
            }

            bool updateRequired = false;
            if (ct != null)
            {
                if (ParameterSpecified(nameof(Name)))
                {
                    ct.Name = Name;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(Description)))
                {
                    ct.Description = Description;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(Group)))
                {
                    ct.Group = Group;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(Hidden)))
                {
                    ct.Hidden = Hidden;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(ReadOnly)))
                {
                    ct.ReadOnly = ReadOnly;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(Sealed)))
                {
                    ct.Sealed = Sealed;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(FormClientSideComponentId)))
                {
                    ct.DisplayFormClientSideComponentId = FormClientSideComponentId;
                    ct.NewFormClientSideComponentId = FormClientSideComponentId;
                    ct.EditFormClientSideComponentId = FormClientSideComponentId;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(FormClientSideComponentProperties)))
                {
                    ct.DisplayFormClientSideComponentProperties = FormClientSideComponentProperties;
                    ct.NewFormClientSideComponentProperties = FormClientSideComponentProperties;
                    ct.EditFormClientSideComponentProperties = FormClientSideComponentProperties;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(DisplayFormClientSideComponentId)))
                {
                    ct.DisplayFormClientSideComponentId = DisplayFormClientSideComponentId;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(DisplayFormClientSideComponentProperties)))
                {
                    ct.DisplayFormClientSideComponentProperties = DisplayFormClientSideComponentProperties;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(NewFormClientSideComponentId)))
                {
                    ct.NewFormClientSideComponentId = NewFormClientSideComponentId;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(NewFormClientSideComponentProperties)))
                {
                    ct.NewFormClientSideComponentProperties = NewFormClientSideComponentProperties;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(EditFormClientSideComponentId)))
                {
                    ct.EditFormClientSideComponentId = EditFormClientSideComponentId;
                    updateRequired = true;
                }

                if (ParameterSpecified(nameof(EditFormClientSideComponentProperties)))
                {
                    ct.EditFormClientSideComponentProperties = EditFormClientSideComponentProperties;
                    updateRequired = true;
                }

                if (updateRequired)
                {
                    if (list != null)
                    {
                        WriteVerbose("Updating content type on list");
                        ct.Update(false);
                    }
                    else
                    {
                        WriteVerbose("Updating site content type");
                        ct.Update(UpdateChildren);
                    }
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(ct);
                }
                else
                {
                    WriteVerbose("No changes to make");
                }
            }
        }
    }
}
