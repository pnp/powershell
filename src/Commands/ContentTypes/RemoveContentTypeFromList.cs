using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{

    [Cmdlet(VerbsCommon.Remove, "PnPContentTypeFromList")]
    public class RemoveContentTypeFromList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetListOrThrow(nameof(List), CurrentWeb);
            var ct = ContentType.GetContentTypeOrWarn(this, list);
            if (ct != null)
            {
                CurrentWeb.RemoveContentTypeFromList(list, ct);
            }
        }

    }
}
