using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPApplicationCustomizer", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class SetApplicationCustomizer : PnPWebCmdlet
    {
        private const string ParameterSet_CUSTOMACTIONID = "Custom Action Id";
        private const string ParameterSet_CLIENTSIDECOMPONENTID = "Client Side Component Id";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_CUSTOMACTIONID)]
        public UserCustomActionPipeBind Identity = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public GuidPipeBind ClientSideComponentId = null;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false)]
        public string Title = null;

        [Parameter(Mandatory = false)]
        public string Description = null;

        [Parameter(Mandatory = false)]
        public int? Sequence = null;

        [Parameter(Mandatory = false)]
        public string ClientSideComponentProperties = null;

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
            
            // If a ClientSideComponentId has been provided, only leave those who have a matching client side component id
            if(ParameterSetName == ParameterSet_CLIENTSIDECOMPONENTID)
            {
                actions = actions.Where(a => a.ClientSideComponentId == ClientSideComponentId.Id && a.Location == "ClientSideExtension.ApplicationCustomizer").ToList();
            }

            if (!actions.Any())
            {
                WriteVerbose($"No CustomAction representing the client side extension registration found within the scope '{Scope}'");
                return;
            }

            // Update each of the matched custom actions
            foreach (var action in actions)
            {
                bool isDirty = false;

                if(Title != null)
                {
                    action.Title = Title;
                    isDirty = true;
                }
                if(Description != null)
                {
                    action.Description = Description;
                    isDirty = true;
                }
                if (Sequence.HasValue)
                {
                    action.Sequence = Sequence.Value;
                    isDirty = true;
                }
                if (ClientSideComponentProperties != null)
                {
                    action.ClientSideComponentProperties = ClientSideComponentProperties;
                    isDirty = true;
                }

                if (isDirty)
                {
                    action.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}