using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Get, "PnPContentType")]
    public class GetContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ContentTypePipeBind Identity;
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ListPipeBind List;
        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter InSiteHierarchy;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List?.GetListOrThrow(nameof(List), CurrentWeb);

                if (Identity != null)
                {

                    var ct = Identity.GetContentTypeOrError(this, nameof(Identity), list);

                    if (ct is null)
                    {
                        return;
                    }

                    WriteObject(ct, false);

                }
                else
                {
                    var cts = ClientContext.LoadQuery(list.ContentTypes.Include(ct => ct.Id, ct => ct.Name, ct => ct.StringId, ct => ct.Group));
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(cts, true);
                }
            }
            else
            {
                if (Identity != null)
                {
                    var ct = Identity.GetContentTypeOrError(this, nameof(Identity), CurrentWeb, InSiteHierarchy);
                    if (ct is null)
                    {
                        return;
                    }
                    WriteObject(ct, false);
                }
                else
                {
                    var cts = InSiteHierarchy ? ClientContext.LoadQuery(CurrentWeb.AvailableContentTypes) : ClientContext.LoadQuery(CurrentWeb.ContentTypes);

                    ClientContext.ExecuteQueryRetry();

                    WriteObject(cts, true);
                }
            }
        }
    }
}

