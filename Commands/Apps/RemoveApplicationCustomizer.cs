using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Remove, "PnPApplicationCustomizer")]
    public class RemoveApplicationCustomizer : PnPWebCmdlet
    {
        private const string ParameterSet_CUSTOMACTIONID = "Custom Action Id";
        private const string ParameterSet_CLIENTSIDECOMPONENTID = "Client Side Component Id";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_CUSTOMACTIONID)]
        public UserCustomActionPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public GuidPipeBind ClientSideComponentId;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.All;

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
                        throw new PSArgumentException($"No CustomAction representing the client side extension registration found with the {(Identity.Id.HasValue ? "Id" : "name")} '{(Identity.Id.HasValue ? Identity.Id.Value.ToString() : Identity.Name)}' within the scope '{Scope}'", "Identity");
                    }
                }
            }

            // Only take the customactions which are application customizers
            actions = actions.Where(a => a.Location == "ClientSideExtension.ApplicationCustomizer").ToList();

            // If a ClientSideComponentId has been provided, only leave those who have a matching client side component id
            if (ParameterSetName == ParameterSet_CLIENTSIDECOMPONENTID)
            {
                actions = actions.Where(a => a.ClientSideComponentId == ClientSideComponentId.Id).ToList();
            }

            if (!actions.Any())
            {
                WriteVerbose($"No CustomAction representing the client side extension registration found within the scope '{Scope}'");
                return;
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