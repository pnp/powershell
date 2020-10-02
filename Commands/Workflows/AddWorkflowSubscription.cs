using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsCommon.Add, "WorkflowSubscription")]
    public class AddWorkflowSubscription : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public string DefinitionName;

        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public SwitchParameter StartManually = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter StartOnCreated;
        
        [Parameter(Mandatory = false)]
        public SwitchParameter StartOnChanged;

        [Parameter(Mandatory = true)]
        public string HistoryListName;

        [Parameter(Mandatory = true)]
        public string TaskListName;

        [Parameter(Mandatory = false)]
        public Dictionary<string,string> AssociationValues;
        
        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(SelectedWeb);

            list.AddWorkflowSubscription(DefinitionName,Name,StartManually,StartOnCreated,StartOnChanged,HistoryListName,TaskListName, AssociationValues);
        }
    }

}
