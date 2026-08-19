using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.ToDo;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsData.Update, "PnPTodoTaskChecklistItem")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [ApiNotAvailableUnderApplicationPermissions]
    public class UpdateTodoTaskChecklistItem : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = true)]
        public TodoTaskPipeBind Task;

        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public bool IsChecked;

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

            var checklistItem = new ChecklistItem();

            if (ParameterSpecified(nameof(DisplayName)))
            {
                checklistItem.DisplayName = DisplayName;
            }
            if (ParameterSpecified(nameof(IsChecked)))
            {
                checklistItem.IsChecked = IsChecked;
            }

            var updatedChecklistItem = ToDoUtility.UpdateChecklistItem(GraphRequestHelper, url, listId, Task.Id, Identity, checklistItem);
            WriteObject(updatedChecklistItem, false);
        }
    }
}
