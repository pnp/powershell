using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System;
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
        public Guid ClientSideComponentId;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.All;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var rawActions = Identity.GetCustomActions(PnPContext, Scope);

            // Only take the customactions which are application customizers
            var actions = rawActions.Where(a => a.Location == "ClientSideExtension.ApplicationCustomizer").ToList();

            // If a ClientSideComponentId has been provided, only leave those who have a matching client side component id
            if (ParameterSetName == ParameterSet_CLIENTSIDECOMPONENTID)
            {
                actions = actions.Where(a => a.ClientSideComponentId == ClientSideComponentId).ToList();
            }

            if (!actions.Any())
            {
                WriteVerbose($"No application customimzers representing the client side extension registration found within the scope '{Scope}'");
                return;
            }

            foreach (var action in actions)
            {
                if (Force || ShouldContinue($"Remove Application Customizer '{action.Name}' with ID {action.Id} at scope {action.Scope}?", Resources.Confirm))
                {
                    action.Delete();
                }
            }
        }
    }
}