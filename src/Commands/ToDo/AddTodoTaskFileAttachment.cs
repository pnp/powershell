using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.ToDo;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Add, "PnPTodoTaskFileAttachment")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.ReadWrite")]
    [ApiNotAvailableUnderApplicationPermissions]
    public class AddTodoTaskFileAttachment : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = true)]
        public TodoTaskPipeBind Task;

        [Parameter(Mandatory = true)]
        public string Path;

        [Parameter(Mandatory = false)]
        public string Name;

        [Parameter(Mandatory = false)]
        public string ContentType;

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

            var resolvedPath = SessionState.Path.GetResolvedPSPathFromPSPath(Path)[0].ProviderPath;
            if (!File.Exists(resolvedPath))
            {
                throw new PSArgumentException("File not found", nameof(Path));
            }

            var fileInfo = new FileInfo(resolvedPath);
            var attachment = new TaskFileAttachment
            {
                Name = ParameterSpecified(nameof(Name)) ? Name : fileInfo.Name,
                ContentType = ParameterSpecified(nameof(ContentType)) ? ContentType : "application/octet-stream",
                ContentBytes = Convert.ToBase64String(File.ReadAllBytes(resolvedPath))
            };

            var createdAttachment = ToDoUtility.CreateTaskFileAttachment(GraphRequestHelper, url, listId, Task.Id, attachment);
            WriteObject(createdAttachment, false);
        }
    }
}
