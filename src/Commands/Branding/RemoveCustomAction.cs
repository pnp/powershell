using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Remove, "PnPCustomAction")]
    public class RemoveCustomAction : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public UserCustomActionPipeBind Identity;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var customActions = Identity.GetCustomActions(Connection.PnPContext, Scope);
            if (customActions != null && customActions.Any())
            {
                foreach (var customAction in customActions)
                {
                    if (Force || ShouldContinue($"Remove custom action '{customAction.Name}' with ID {customAction.Id} at scope {customAction.Scope}?", Resources.Confirm))
                    {
                        customAction.Delete();
                    }
                }
            }
        }
    }
}
