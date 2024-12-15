using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System.Linq.Expressions;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Get, "PnPContentType")]
    [OutputType(typeof(ContentType))]
    [OutputType(typeof(IEnumerable<ContentType>))]
    public class GetContentType : PnPWebRetrievalsCmdlet<ContentType>
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
         [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter InSiteHierarchy;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<ContentType, object>>[] { ct => ct.Id, ct => ct.Name, ct => ct.StringId, ct => ct.Group };

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

                    ct.EnsureProperties(RetrievalExpressions);
                    WriteObject(ct, false);
                }
                else
                {
                    var cts = ClientContext.LoadQuery(list.ContentTypes.IncludeWithDefaultProperties(RetrievalExpressions));
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

                    ct.EnsureProperties(RetrievalExpressions);
                    WriteObject(ct, false);
                }
                else
                {
                    var cts = InSiteHierarchy ? ClientContext.LoadQuery(CurrentWeb.AvailableContentTypes.IncludeWithDefaultProperties(RetrievalExpressions)) : ClientContext.LoadQuery(CurrentWeb.ContentTypes.IncludeWithDefaultProperties(RetrievalExpressions));

                    ClientContext.ExecuteQueryRetry();

                    WriteObject(cts, true);
                }
            }
        }
    }
}
