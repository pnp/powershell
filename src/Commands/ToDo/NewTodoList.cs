using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using System.Net.Http;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.New, "PnPTodoList")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite.All")]
    public class NewTodoList : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string DisplayName;

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
                url = $"/v1.0/users/{user.Id}/todo/lists";
            }

            var stringContent = new StringContent($"{{'displayName':'{DisplayName}'}}");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var todoList = GraphRequestHelper.Post<Model.ToDo.ToDoList>(url, stringContent);
            WriteObject(todoList, false);
        }
    }
}
