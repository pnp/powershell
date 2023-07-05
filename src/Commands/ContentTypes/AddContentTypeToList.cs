using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPContentTypeToList")]
    public class AddContentTypeToList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public SwitchParameter DefaultContentType;

        protected override void ExecuteCmdlet()
        {
            List list = List.GetList(CurrentWeb);
            var ct = ContentType?.GetContentTypeOrWarn(this, CurrentWeb);
            if (ct != null)
            {
                list.AddContentTypeToList(ct, DefaultContentType);
            }
        }

    }
}