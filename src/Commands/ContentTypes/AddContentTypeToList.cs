using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPContentTypeToList")]
    public class AddContentTypeToList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
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