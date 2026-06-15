using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
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
        public EntraIDUserPipeBind User;

        protected override void ExecuteCmdlet()
        {
            var url = ToDoUtility.GetTodoRootUrl(this, ParameterSpecified(nameof(User)) ? User : null);
            if (url == null)
            {
                return;
            }

            var graphResult = ToDoUtility.DeleteList(GraphRequestHelper, url, Identity);

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
