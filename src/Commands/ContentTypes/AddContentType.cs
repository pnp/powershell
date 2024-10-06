using Microsoft.SharePoint.Client;
using System.Management.Automation;


namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPContentType")]
    public class AddContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = false)]
        public string ContentTypeId;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Group;

        [Parameter(Mandatory = false)]
        public ContentType ParentContentType;

        [Parameter(Mandatory = false)]
        public string DocumentTemplate;

        protected override void ExecuteCmdlet()
        {
            var ct = CurrentWeb.CreateContentType(Name, Description, ContentTypeId, Group, ParentContentType);

            if (ParameterSpecified(nameof(DocumentTemplate)))
            {
                ct.DocumentTemplate = DocumentTemplate;
                ct.Update(true);
            }

            ClientContext.Load(ct);
            ClientContext.ExecuteQueryRetry();
            WriteObject(ct);
        }
    }
}
