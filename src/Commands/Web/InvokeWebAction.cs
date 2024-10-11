using Microsoft.SharePoint.Client;
using Csomweb = Microsoft.SharePoint.Client.Web;

using PnP.PowerShell.Commands.InvokeAction;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPWebAction", SupportsShouldProcess = true)]
    [OutputType(typeof(InvokeWebActionResult), typeof(System.Data.DataTable))]
    public class InvokeWebAction : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public Csomweb[] Webs;

        [Parameter(Mandatory = false)]
        public Action<Csomweb> WebAction;

        [Parameter(Mandatory = false)]
        public Func<Csomweb, bool> ShouldProcessWebAction;

        [Parameter(Mandatory = false)]
        public Action<Csomweb> PostWebAction;

        [Parameter(Mandatory = false)]
        public Func<Csomweb, bool> ShouldProcessPostWebAction;

        [Parameter(Mandatory = false)]
        public string[] WebProperties;

        [Parameter(Mandatory = false)]
        public string ListName { get; set; }

        [Parameter(Mandatory = false)]
        public Action<List> ListAction;

        [Parameter(Mandatory = false)]
        public Func<List, bool> ShouldProcessListAction;

        [Parameter(Mandatory = false)]
        public Action<List> PostListAction;

        [Parameter(Mandatory = false)]
        public Func<List, bool> ShouldProcessPostListAction;

        [Parameter(Mandatory = false)]
        public string[] ListProperties;

        [Parameter(Mandatory = false)]
        public Action<ListItem> ListItemAction;

        [Parameter(Mandatory = false)]
        public Func<ListItem, bool> ShouldProcessListItemAction;

        [Parameter(Mandatory = false)]
        public string[] ListItemProperties;

        [Parameter(Mandatory = false)]
        public SwitchParameter SubWebs;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisableStatisticsOutput;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipCounting;

        protected override void ExecuteCmdlet()
        {
            if (WebAction == null && ListAction == null && ListItemAction == null && PostWebAction == null && PostListAction == null)
            {
                ThrowTerminatingError(new ErrorRecord(new ArgumentNullException("An action need to be specified"), "0", ErrorCategory.InvalidArgument, null));
                return;
            }

            InvokeActionParameter<Csomweb> webActions = new InvokeActionParameter<Csomweb>()
            {
                Action = WebAction,
                ShouldProcessAction = ShouldProcessWebAction,
                PostAction = PostWebAction,
                ShouldProcessPostAction = ShouldProcessPostWebAction,
                Properties = WebProperties
            };

            InvokeActionParameter<List> listActions = new InvokeActionParameter<List>()
            {
                Action = ListAction,
                ShouldProcessAction = ShouldProcessListAction,
                PostAction = PostListAction,
                ShouldProcessPostAction = ShouldProcessPostListAction,
                Properties = ListProperties
            };

            InvokeActionParameter<ListItem> listItemActions = new InvokeActionParameter<ListItem>()
            {
                Action = ListItemAction,
                ShouldProcessAction = ShouldProcessListItemAction,
                Properties = WebProperties
            };

            InvokeAction.InvokeWebAction invokeAction;
            if (string.IsNullOrEmpty(ListName))
            {
                IEnumerable<Csomweb> websToProcess;
                if (Webs == null || Webs.Length == 0)
                    websToProcess = new[] { CurrentWeb };
                else
                    websToProcess = Webs;

                invokeAction = new InvokeAction.InvokeWebAction(this, websToProcess, SubWebs.ToBool(), webActions, listActions, listItemActions, SkipCounting.ToBool());
            }
            else
            {
                invokeAction = new InvokeAction.InvokeWebAction(this, CurrentWeb, ListName, webActions, listActions, listItemActions, SkipCounting.ToBool());
            }

            InvokeWebActionResult result = invokeAction.StartProcessAction(Connection);

            if (!DisableStatisticsOutput)
            {
                WriteObject(result.ToDataTable());
            }

            WriteObject(result);
        }
    }
}