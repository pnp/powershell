using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Get, "PnPTodoTaskFileAttachment")]
    [RequiredApiDelegatedPermissions("graph/Tasks.Read")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read.All")]
    public class GetTodoTaskFileAttachment : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = true)]
        public TodoTaskPipeBind Task;

        [Parameter(Mandatory = false)]
        public TodoTaskFileAttachmentPipeBind Identity;

        [Parameter(Mandatory = false)]
        public EntraIDUserPipeBind User;

        [Parameter(Mandatory = false)]
        public SwitchParameter DoNotIncludeFileContent;

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
                var attachment = ToDoUtility.GetTaskFileAttachment(GraphRequestHelper, url, listId, Task.Id, Identity.Id, !DoNotIncludeFileContent);
                WriteObject(attachment, false);
            }
            else
            {
                var attachments = ToDoUtility.GetTaskFileAttachments(GraphRequestHelper, url, listId, Task.Id, !DoNotIncludeFileContent);
                WriteObject(attachments, true);
            }
        }
    }
}
