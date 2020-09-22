using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
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
            List<UserCustomAction> actions = new List<UserCustomAction>();

            if (Identity != null && Identity.UserCustomAction != null)
            {
                actions.Add(Identity.UserCustomAction);
            }
            else
            {
                if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
                {
                    actions.AddRange(SelectedWeb.GetCustomActions());
                }
                if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
                {
                    actions.AddRange(ClientContext.Site.GetCustomActions());
                }

                if (Identity != null)
                {
                    actions = actions.Where(action => Identity.Id.HasValue ? Identity.Id.Value == action.Id : Identity.Name == action.Name).ToList();

                    if (!actions.Any())
                    {
                        throw new PSArgumentException($"No CustomAction found with the {(Identity.Id.HasValue ? "Id" : "name")} '{(Identity.Id.HasValue ? Identity.Id.Value.ToString() : Identity.Name)}' within the scope '{Scope}'", "Identity");
                    }
                }

                if (!actions.Any())
                {
                    WriteVerbose($"No CustomAction found within the scope '{Scope}'");
                    return;
                }
            }

            foreach (var action in actions.Where(action => Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Resources.RemoveCustomAction, action.Name, action.Id, action.Scope), Resources.Confirm)))
            {
                switch (action.Scope)
                {
                    case UserCustomActionScope.Web:
                        SelectedWeb.DeleteCustomAction(action.Id);
                        break;

                    case UserCustomActionScope.Site:
                        ClientContext.Site.DeleteCustomAction(action.Id);
                        break;
                }
            }
        }
    }
}
