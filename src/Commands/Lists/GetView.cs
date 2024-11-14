using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;

using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPView")]
    [OutputType(typeof(View))]
    public class GetView : PnPWebRetrievalsCmdlet<View>
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public ViewPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<View, object>>[] { v => v.Id, v => v.Title, v => v.ServerRelativeUrl, v => v.DefaultView, v => v.PersonalView, v => v.ViewFields };

            if (List != null)
            {
                var list = List.GetList(CurrentWeb);
                if (list != null)
                {
                    View view = null;
                    IEnumerable<View> views = null;
                    if (Identity != null)
                    {
                        if (Identity.Id != Guid.Empty)
                        {
                            view = list.GetViewById(Identity.Id, RetrievalExpressions);
                            view.EnsureProperties(RetrievalExpressions);

                        }
                        else if (!string.IsNullOrEmpty(Identity.Title))
                        {
                            view = list.GetViewByName(Identity.Title, RetrievalExpressions);
                            view.EnsureProperties(RetrievalExpressions);
                        }
                    }
                    else
                    {
                        views = ClientContext.LoadQuery(list.Views.IncludeWithDefaultProperties(RetrievalExpressions));
                        ClientContext.ExecuteQueryRetry();

                    }
                    if (views != null && views.Any())
                    {
                        WriteObject(views, true);
                    }
                    else if (view != null)
                    {
                        WriteObject(view);
                    }
                }
            }

        }
    }

}
