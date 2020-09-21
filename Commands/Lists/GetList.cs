using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq.Expressions;
using System;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPList")]
    public class GetList : PnPWebRetrievalsCmdlet<List>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter ThrowExceptionIfListNotFound;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<List, object>>[] { l => l.Id, l => l.BaseTemplate, l => l.OnQuickLaunch, l => l.DefaultViewUrl, l => l.Title, l => l.Hidden, l => l.RootFolder.ServerRelativeUrl };

            if (Identity != null)
            {
                var list = Identity.GetList(SelectedWeb);

                if (ThrowExceptionIfListNotFound && list == null)
                { 
                    throw new PSArgumentException(string.Format(Resources.ListNotFound, Identity), nameof(Identity));
                }
                list?.EnsureProperties(RetrievalExpressions);

                WriteObject(list);
            }
            else
            {
                var query = SelectedWeb.Lists.IncludeWithDefaultProperties(RetrievalExpressions);
                var lists = ClientContext.LoadQuery(query);
                ClientContext.ExecuteQueryRetry();
                WriteObject(lists, true);
            }
        }
    }
}
