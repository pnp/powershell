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
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public SwitchParameter DefaultContentType;

        protected override void ExecuteCmdlet()
        {
            ContentType ct = null;
            List list = List.GetList(SelectedWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            if (ContentType.ContentType == null)
            {
                if (ContentType.Id != null)
                {
                    ct = SelectedWeb.GetContentTypeById(ContentType.Id, true);
                }
                else if (ContentType.Name != null)
                {
                    ct = SelectedWeb.GetContentTypeByName(ContentType.Name, true);
                }
            }
            else
            {
                ct = ContentType.ContentType;
            }
            if (ct != null)
            {
                SelectedWeb.AddContentTypeToList(list.Title, ct, DefaultContentType);
            }
        }

    }
}
