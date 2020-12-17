using System.Management.Automation;
using Microsoft.SharePoint.Client;


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

        protected override void ExecuteCmdlet()
        {
            var ct = CurrentWeb.CreateContentType(Name, Description, ContentTypeId, Group, ParentContentType);
            ClientContext.Load(ct);
            ClientContext.ExecuteQueryRetry();
            WriteObject(ct);
        }


    }
}
