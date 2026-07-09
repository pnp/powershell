using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

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
        public EntraIDUserPipeBind User;

        protected override void ExecuteCmdlet()
        {
            var url = ToDoUtility.GetTodoRootUrl(this, ParameterSpecified(nameof(User)) ? User : null);
            if (url == null)
            {
                return;
            }

            var todoList = ToDoUtility.UpdateList(GraphRequestHelper, url, Identity, DisplayName);
            WriteObject(todoList, false);
        }
    }
}
