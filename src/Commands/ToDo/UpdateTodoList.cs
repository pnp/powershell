using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using System.Net.Http;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsData.Update, "PnPTodoList")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.ReadWrite")]
    [ApiNotAvailableUnderApplicationPermissions]
    public class UpdateTodoList : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public AzureADUserPipeBind User;

        protected override void ExecuteCmdlet()
        {
            string url = $"/v1.0/me/todo/lists/{Identity}";

            if (ParameterSpecified(nameof(User)))
            {
                var user = User.GetUser(AccessToken, Connection.AzureEnvironment);
                if (user == null)
                {
                    WriteWarning("Provided user not found");
                    return;
                }
                url = $"/v1.0/users/{user.Id}/todo/lists/{Identity}";
            }

            var stringContent = new StringContent($"{{'displayName':'{DisplayName}'}}");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var todoList = GraphRequestHelper.Patch<Model.ToDo.ToDoList>(url, stringContent);
            WriteObject(todoList, false);
        }
    }
}
