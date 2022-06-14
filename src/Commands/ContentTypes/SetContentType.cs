using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Set, "PnPContentType")]
    public class SetContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ContentTypePipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter InSiteHierarchy;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter UpdateChildren;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public string Name;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public string Description;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public string Group;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public bool Hidden;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public bool ReadOnly;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public bool Sealed;

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

                if (updateRequired)
                {
                    if(list != null)
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
