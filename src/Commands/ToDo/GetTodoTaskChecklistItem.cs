using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Get, "PnPTodoTaskChecklistItem")]
    [RequiredApiDelegatedPermissions("graph/Tasks.Read")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read.All")]
    public class GetTodoTaskChecklistItem : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = true)]
        public TodoTaskPipeBind Task;

        [Parameter(Mandatory = false)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public EntraIDUserPipeBind User;

        protected override void ExecuteCmdlet()
        {
            var url = ToDoUtility.GetTodoRootUrl(this, ParameterSpecified(nameof(User)) ? User : null);
            if (url == null)
            {
                return;
            }

            var listId = ToDoUtility.GetListId(GraphRequestHelper, url, List);
            if (listId == null)
            {
                throw new PSArgumentException("Todo list not found", nameof(List));
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var checklistItem = ToDoUtility.GetChecklistItem(GraphRequestHelper, url, listId, Task.Id, Identity);
                WriteObject(checklistItem, false);
            }
            else
            {
                var checklistItems = ToDoUtility.GetChecklistItems(GraphRequestHelper, url, listId, Task.Id);
                WriteObject(checklistItems, true);
            }
        }
    }
}
