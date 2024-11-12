using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPView")]
    [OutputType(typeof(View))]
    public class AddView : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]

        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Query;

        [Parameter(Mandatory = true)]
        public string[] Fields;

        [Parameter(Mandatory = false)]
        public ViewType ViewType = ViewType.None;

        [Parameter(Mandatory = false)]
        public uint RowLimit = 30;

        [Parameter(Mandatory = false)]
        public SwitchParameter Personal;

        [Parameter(Mandatory = false)]
        public SwitchParameter SetAsDefault;

        [Parameter(Mandatory = false)]
        public SwitchParameter Paged;

        [Parameter(Mandatory = false)]
        public string Aggregations;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list != null)
            {
                var view = list.CreateView(Title, ViewType, Fields, RowLimit, SetAsDefault, Query, Personal, Paged);

                if (ParameterSpecified(nameof(Aggregations)))
                {
                    view.Aggregations = Aggregations;
                    view.Update();
                    list.Context.Load(view);
                    list.Context.ExecuteQueryRetry();
                }
                WriteObject(view);
            }
        }
    }
}
