using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Mail;
using PnP.PowerShell.Commands.Model.ToDo;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.New, "PnPTodoTask")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite.All")]
    public class NewTodoTask : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true), ArgumentCompleter(typeof(TodoListCompleter))]
        public string List;

        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Body;

        [Parameter(Mandatory = false)]
        public MessageBodyContentType BodyContentType = MessageBodyContentType.Text;

        [Parameter(Mandatory = false)]
        public string[] Categories;

        [Parameter(Mandatory = false)]
        public DateTime DueDateTime;

        [Parameter(Mandatory = false)]
        public DateTime StartDateTime;

        [Parameter(Mandatory = false)]
        public DateTime ReminderDateTime;

        [Parameter(Mandatory = false)]
        public ToDoTaskImportance Importance;

        [Parameter(Mandatory = false)]
        public ToDoTaskStatus Status;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsReminderOn;

        [Parameter(Mandatory = false)]
        public string TimeZone = "UTC";

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

            var todoTask = new ToDoTask
            {
                Title = Title
            };

            if (ParameterSpecified(nameof(Body)))
            {
                todoTask.Body = new Body { Content = Body, ContentType = BodyContentType };
            }
            if (ParameterSpecified(nameof(Categories)))
            {
                todoTask.Categories = Categories;
            }
            if (ParameterSpecified(nameof(DueDateTime)))
            {
                todoTask.DueDateTime = new DateTimeTimeZone { DateTime = DueDateTime.ToString("s"), TimeZone = TimeZone };
            }
            if (ParameterSpecified(nameof(StartDateTime)))
            {
                todoTask.StartDateTime = new DateTimeTimeZone { DateTime = StartDateTime.ToString("s"), TimeZone = TimeZone };
            }
            if (ParameterSpecified(nameof(ReminderDateTime)))
            {
                todoTask.ReminderDateTime = new DateTimeTimeZone { DateTime = ReminderDateTime.ToString("s"), TimeZone = TimeZone };
            }
            if (ParameterSpecified(nameof(Importance)))
            {
                todoTask.Importance = Importance;
            }
            if (ParameterSpecified(nameof(Status)))
            {
                todoTask.Status = Status;
            }
            if (ParameterSpecified(nameof(IsReminderOn)))
            {
                todoTask.IsReminderOn = IsReminderOn;
            }

            var createdTask = ToDoUtility.CreateTask(GraphRequestHelper, url, listId, todoTask);
            WriteObject(createdTask, false);
        }
    }
}
