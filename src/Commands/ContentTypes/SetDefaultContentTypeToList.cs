using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Set, "PnPDefaultContentTypeToList")]
    public class SetDefaultContentTypeToList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            ContentType ct = null;
            List list = List.GetList(SelectedWeb);

            if (ContentType.ContentType == null)
            {
                if (ContentType.Id != null)
                {
                    ct = list.GetContentTypeById(ContentType.Id);
                }
                else if (ContentType.Name != null)
                {
                    ct = list.GetContentTypeByName(ContentType.Name);
                }
            }
            else
            {
                ct = ContentType.ContentType;
                if(!list.ContentTypeExistsById(ct.Id.ToString()))
                {
                    WriteError(new ErrorRecord(new System.Exception("Content type does not exist on list. Use Add-PnPContentTypeToList to add the content type first."), "CONTENTTYPENOTADDEDTOLIST", ErrorCategory.ResourceUnavailable, ct));
                }
            }
            if (ct != null)
            {
                list.SetDefaultContentType(ct.Id);
            }
        }

    }
}
