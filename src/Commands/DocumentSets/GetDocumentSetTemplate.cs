using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.DocumentSet;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Get,"PnPDocumentSetTemplate")]
    [OutputType(typeof(DocumentSetTemplate))]
    public class GetDocumentSetTemplate : PnPWebRetrievalsCmdlet<DocumentSetTemplate>
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public DocumentSetPipeBind Identity;

        protected override void ExecuteCmdlet()
        { 
            DefaultRetrievalExpressions = new Expression<Func<DocumentSetTemplate, object>>[] { t => t.AllowedContentTypes, t => t.DefaultDocuments, t => t.SharedFields, t => t.WelcomePageFields };

            var docSetTemplate = Identity.GetDocumentSetTemplate(CurrentWeb);

            ClientContext.Load(docSetTemplate, RetrievalExpressions);

            ClientContext.ExecuteQueryRetry();

            WriteObject(docSetTemplate);
        }
    }
}
