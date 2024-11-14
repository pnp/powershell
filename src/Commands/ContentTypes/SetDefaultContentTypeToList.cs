using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Set, "PnPDefaultContentTypeToList")]
    public class SetDefaultContentTypeToList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetListOrThrow(nameof(List), CurrentWeb);
            var ctId = ContentType.GetIdOrThrow(nameof(ContentType), list);

            list.SetDefaultContentType(ctId);
        }

    }
}
