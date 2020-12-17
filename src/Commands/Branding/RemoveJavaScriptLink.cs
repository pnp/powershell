using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

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
            List<UserCustomAction> actions = new List<UserCustomAction>();

            if (Identity != null && Identity.UserCustomAction != null && Identity.UserCustomAction.Location == "ScriptLink")
            {
                actions.Add(Identity.UserCustomAction);
            }
            else
            {
                if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
                {
                    actions.AddRange(CurrentWeb.GetCustomActions().Where(c => c.Location == "ScriptLink"));
                }
                if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
                {
                    actions.AddRange(ClientContext.Site.GetCustomActions().Where(c => c.Location == "ScriptLink"));
                }

                if (Identity != null)
                {
                    actions = actions.Where(action => Identity.Id.HasValue ? Identity.Id.Value == action.Id : Identity.Name == action.Name).ToList();

                    if (!actions.Any())
                    {
                        throw new ArgumentException($"No JavaScriptLink found with the {(Identity.Id.HasValue ? "Id" : "name")} '{(Identity.Id.HasValue ? Identity.Id.Value.ToString() : Identity.Name)}' within the scope '{Scope}'", "Identity");
                    }
                }

                if (!actions.Any())
                {
                    WriteVerbose($"No JavaScriptLink registrations found within the scope '{Scope}'");
                    return;
                }
            }

            foreach (var action in actions.Where(action => Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Resources.RemoveJavaScript0, action.Name, action.Id, action.Scope), Resources.Confirm)))
            {
                switch (action.Scope)
                {
                    case UserCustomActionScope.Web:
                        CurrentWeb.DeleteJsLink(action.Name);
                        break;

                    case UserCustomActionScope.Site:
                        ClientContext.Site.DeleteJsLink(action.Name);
                        break;
                }
            }
        }
    }
}