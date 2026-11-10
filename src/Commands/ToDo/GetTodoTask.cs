using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Get, "PnPTodoTask")]
    [RequiredApiDelegatedPermissions("graph/Tasks.Read")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read.All")]
    public class GetTodoTask : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public TodoTaskPipeBind Identity;

        [Parameter(Mandatory = false)]
        public EntraIDUserPipeBind User;

        protected override void ExecuteCmdlet()
        {
            var url = ToDoUtility.GetTodoRootUrl(this, ParameterSpecified(nameof(User)) ? User : null);
            if (url == null)
            {
                return;
            }

            var todoList = ToDoUtility.GetList(GraphRequestHelper, url, List);
            if (todoList == null)
            {
                throw new PSArgumentException("Todo list not found", nameof(List));
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var todoTask = ToDoUtility.GetTask(GraphRequestHelper, url, todoList.Id, Identity.Id);
                WriteObject(todoTask, false);
            }
            else
            {
                var todoTasks = ToDoUtility.GetTasks(GraphRequestHelper, url, todoList.Id);
                WriteObject(todoTasks, true);
            }
        }
    }
}
