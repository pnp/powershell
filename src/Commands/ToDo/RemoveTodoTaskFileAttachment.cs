using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Remove, "PnPTodoTaskFileAttachment")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [ApiNotAvailableUnderApplicationPermissions]
    public class RemoveTodoTaskFileAttachment : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = true)]
        public TodoTaskPipeBind Task;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TodoTaskFileAttachmentPipeBind Identity;

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

            var graphResult = ToDoUtility.DeleteTaskFileAttachment(GraphRequestHelper, url, listId, Task.Id, Identity.Id);

            if (graphResult.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                LogDebug("Todo task file attachment deleted successfully");
            }
            else
            {
                throw new PSArgumentException("Todo task file attachment could not be deleted", nameof(Identity));
            }
        }
    }
}
