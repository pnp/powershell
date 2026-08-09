using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Get, "PnPTodoList")]
    [RequiredApiDelegatedPermissions("graph/Tasks.Read")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read.All")]
    public class GetTodoList : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false), ArgumentCompleter(typeof(TodoListCompleter))]
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

            if (ParameterSpecified(nameof(Identity)))
            {
                var todoList = ToDoUtility.GetList(GraphRequestHelper, url, Identity);
                if (todoList == null)
                {
                    throw new PSArgumentException("Todo list not found", nameof(Identity));
                }

                WriteObject(todoList, false);
            }
            else
            {
                var todoLists = ToDoUtility.GetLists(GraphRequestHelper, url);
                WriteObject(todoLists, true);
            }
        }
    }
}
