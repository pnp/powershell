using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ToDo
{
    [Cmdlet(VerbsCommon.Remove, "PnPTodoList")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Tasks.ReadWrite")]
    [ApiNotAvailableUnderApplicationPermissions]
    public class RemoveTodoList : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

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
                    LogWarning("Provided user not found");
                    return;
                }
                url = $"/v1.0/users/{user.Id.Value}/todo/lists/{Identity}";
            }

            var graphResult = GraphRequestHelper.Delete(url);

            if (graphResult.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                LogDebug("Todo list deleted successfully");
            }
            else
            {
                throw new PSArgumentException("Todo list could not be deleted", nameof(Identity));
            }
        }
    }
}
