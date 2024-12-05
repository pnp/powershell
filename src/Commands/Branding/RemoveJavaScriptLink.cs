using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Remove, "PnPJavaScriptLink")]
    public class RemoveJavaScriptLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public UserCustomActionPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        protected override void ExecuteCmdlet()
        {
            var rawActions = Identity.GetCustomActions(Connection.PnPContext, Scope);

            var actions = rawActions.Where(ca => ca.Location == "ScriptLink");

            foreach (var action in actions)
            {
                if (Force || ShouldContinue($"Remove JavaScript Link '{action.Name} with ID {action.Id} at scope {action.Scope}?", Resources.Confirm))
                {
                    action.Delete();
                }
            }
        }
    }
}