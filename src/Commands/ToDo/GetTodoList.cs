using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Get, "PnPTodoList")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.Read")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read.All")]
    public class GetTodoList : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public AzureADUserPipeBind User;
        protected override void ExecuteCmdlet()
        {
            string url = "/v1.0/me/todo/lists";

            if (Connection.ConnectionMethod == Model.ConnectionMethod.AzureADAppOnly)
            {
                if (!ParameterSpecified(nameof(User)))
                {
                    throw new PSInvalidOperationException($"Please specify the parameter {nameof(User)} when invoking this cmdlet in app-only scenario");
                }
            }

            if (ParameterSpecified(nameof(User)))
            {
                var user = User.GetUser(AccessToken, Connection.AzureEnvironment);
                if (user == null)
                {
                    LogWarning("Provided user not found");
                    return;
                }
                url = $"/v1.0/users/{user.Id.Value}/todo/lists";
            }
            if (ParameterSpecified(nameof(Identity)))
            {
                url += $"/{Identity}";

                var todoList = GraphRequestHelper.Get<Model.ToDo.ToDoList>(url);
                WriteObject(todoList, false);
            }
            else
            {
                var todoLists = GraphRequestHelper.GetResultCollection<Model.ToDo.ToDoList>(url);
                WriteObject(todoLists, true);
            }
        }
    }
}
